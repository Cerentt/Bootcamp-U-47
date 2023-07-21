using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f; 
    public GameObject DeathParticles;

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
        if (health <= 0f)
        {
            Die();
        }
    }


    //It was changed to kill and spawn new zombies - added
    void Die()
    {
        GameObject deadZombie = ZombieSpawner.instance.GetDeadZombieGO();

        //Zombie Death animation
        animator.SetFloat("Health", deadZombie.GetComponent<Target>().health);

        Invoke("ZombieSetActive", 3f);

        Invoke("ZombieRegeneration", 4f);
    
        //Instantiate(DeathParticles, transform.position, Quaternion.identity);
    }
    

    //Methods -- added

    void ZombieSetActive()
    {
        ZombieSpawner.instance.GetDeadZombieGO().SetActive(false);
    }

    void ZombieRegeneration()
    {
        GameObject deadZombie = ZombieSpawner.instance.GetDeadZombieGO();

        deadZombie.GetComponent<Target>().health = 100f;
        deadZombie.GetComponent<ZombieAI>().sightRange = 15f;
    }

}
