using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;

    [SerializeField] private List<Transform> spawnPointList = new List<Transform>();
    [SerializeField] private GameObject zombiePrefab;
    private List<GameObject> spawnedZombieGOList = new List<GameObject>();
    
    private float spawnTimer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        spawnTimer = 20f;

        for(int i=0; i < spawnPointList.Count; i++)
        {
            GameObject zombieObject = Instantiate(zombiePrefab, spawnPointList[i]);
            zombieObject.SetActive(true);
            spawnedZombieGOList.Add(zombieObject);
        }
    }

    private void Update()
    {
        SpawnZombieAgain();
    }

    private void SpawnZombieAgain()
    {
        for (int i = 0; i < spawnedZombieGOList.Count; i++)
        {
            GameObject zombieObject = spawnedZombieGOList[i];

            if (!zombieObject.activeInHierarchy)
            {
                spawnTimer -= Time.deltaTime;

                if (spawnTimer <= 0)
                {
                    zombieObject.transform.position = spawnPointList[i].position;
                    zombieObject.SetActive(true);
                    spawnTimer = 20f;
                }
            }
        }
    }

    public GameObject GetDeadZombieGO()
    {
        for (int i = 0; i < spawnedZombieGOList.Count; i++)
        {
            Target target = spawnedZombieGOList[i].GetComponent<Target>();

            if (target.health <= 0)
            {
                return spawnedZombieGOList[i];
            }
        }

        return null;
    }


}
