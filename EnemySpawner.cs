using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections.Generic;
using Unity.Jobs;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] Transform minSpawn;
    [SerializeField] Transform maxSpawn;
    [SerializeField] int checkPerFrame = 10;
    [SerializeField] private float despawnDistanceValue = 5f;

    public List<WaveInfo> waves;

    private float spawnTimer;
    private Transform playerTransform;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private int enemyToCheck;
    private float despawnDistance;
    private int currentWaveIndex;
    private float waveTimer;

    private void Start()
    {
        //spawnTimer = spawnInterval;
        playerTransform = PlayerHealthController.instance.transform;
        despawnDistance = Vector3.Distance(minSpawn.position, maxSpawn.position) + despawnDistanceValue;
        currentWaveIndex = -1;
        GoToNextWave();
    }

    private void Update()
    {
        //spawnTimer -= Time.deltaTime;
        //if (spawnTimer <= 0f)
        //{
        //    SpawnEnemy();
        //    spawnTimer = spawnInterval;
        //}

        if(PlayerHealthController.instance.gameObject.activeSelf)
        {
            if(currentWaveIndex < waves.Count)
            {
                waveTimer -= Time.deltaTime;

                if(waveTimer <= 0f)
                {
                    GoToNextWave();
                }

                spawnTimer -= Time.deltaTime;

                if(spawnTimer <= 0f)
                {
                    SpawnEnemy();
                    spawnTimer = waves[currentWaveIndex].timeBetweenSpawns;
                }
            }
        }

        transform.position = playerTransform.position;

        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget)
        {
            if(enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if(Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(waves[currentWaveIndex].enemyToSpawn, GetSpawnPosition(), Quaternion.identity);
        spawnedEnemies.Add(newEnemy);
    }

    public Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;

        if (spawnVerticalEdge)
        {
            spawnPosition.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPosition.x = minSpawn.position.x;
            }
            else
            {
                spawnPosition.x = maxSpawn.position.x;
            }
        }
        else
        {
            spawnPosition.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPosition.y = minSpawn.position.y;
            }
            else
            {
                spawnPosition.y = maxSpawn.position.y;
            }
        }

        return spawnPosition;
    }

    public void GoToNextWave()
    {
        currentWaveIndex++;

        if(currentWaveIndex >= waves.Count)
        {
            currentWaveIndex = waves.Count - 1;
        }

        waveTimer = waves[currentWaveIndex].waveLength;
        spawnTimer= waves[currentWaveIndex].timeBetweenSpawns;
    }
}

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
