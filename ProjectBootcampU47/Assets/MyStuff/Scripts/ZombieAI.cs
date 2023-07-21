using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] LayerMask groundLayerMask, playerLayerMask;
    [SerializeField] private float walkPointRange = 5f;
    [SerializeField] private float attackRange = 0.8f;
    public float sightRange = 15f;

    private NavMeshAgent agent;

    private Vector3 destinationPoint;
    private bool destinationPointSet;
    private bool playerInSightRange, playerInAttackRange;

    public bool isRoaming;
    public bool isChasing;
    public bool isAttacking;

    private float maxDestinationDistance = 2.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayerMask);

        ControlPlayerState();
    }

    private void Roaming()
    {
        isRoaming = true;
        isChasing = false;
        isAttacking = false;

        if (!destinationPointSet)
        {
            SearchWalkPoint();
        }
        if (destinationPointSet)
        {
            agent.SetDestination(destinationPoint);
        }

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        
        if(distanceToDestinationPoint.magnitude < 1f)
        {
            destinationPointSet = false;
        }

        sightRange = 15f;
    }

    private void ChasePlayer()
    {
        isRoaming = false;
        isChasing = true;
        isAttacking = false;

        agent.SetDestination(player.position);

        sightRange = 30f;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        isRoaming = false;
        isChasing = false;
        isAttacking = true;
    }

    private void SearchWalkPoint()
    {
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(destinationPoint, -transform.up, maxDestinationDistance, groundLayerMask))
        {
            destinationPointSet = true;
        }
    }

    private void ControlPlayerState()
    {
        if(!playerInSightRange && !playerInAttackRange)
        {
            Roaming();        
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if(playerInAttackRange)
        {
            AttackPlayer();
        }
    }



}
