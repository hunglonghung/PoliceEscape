using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnner : MonoBehaviour
{
    public GameObject player;
    public static EnemySpawnner Instance;
    [SerializeField] private int poolSize = 5;
    private int spawnedBotNumber = 0;
    [SerializeField] private int targetNumber = 50;
    [SerializeField] public List<GameObject> SpawnedEnemies;
    private List<GameObject> enemyPools;
    public float spawnRadius = 40f;
    public float minDistance = 10f;
    
    // Bộ đếm thời gian cho mỗi loại kẻ thù
    [SerializeField] private float planeSpawnTimer = 0f;
    [SerializeField] private float carSpawnTimer = 0f;
    [SerializeField] private float vanSpawnTimer = 0f;
    [SerializeField] private float truckSpawnTimer = 0f;
    [SerializeField] private float tankSpawnTimer = 0f;
    [SerializeField] private float maxDistanceToSpawn = 50f;
    [SerializeField] private float maxDistanceToPlayer = 100f;

    private void Awake()
    {
        Instance = this;
        enemyPools = new List<GameObject>();
    }

    private void Start() 
    {
        enemyPools = EnemyPooling.Instance.enemyPrefabs;
        SpawnBot();
    }

    private void Update() 
    {
        
        HandleSpawnTimers();
        RelocateDistantEnemies();
    }

    private Vector3 GetValidPosition()
    {
        Vector3 randomPosition;
        bool validPosition;

        do
        {
            validPosition = true;
            randomPosition = UnityEngine.Random.insideUnitCircle * spawnRadius;
            randomPosition += transform.position;
            randomPosition.y = 2;
            foreach (GameObject spawnedBot in enemyPools)
            {
                if (spawnedBot != null && Vector3.Distance(randomPosition, spawnedBot.transform.position) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }
            // Player check
            if (Vector3.Distance(randomPosition, player.transform.position) < minDistance)
            {
                validPosition = false;
            }

        } while (!validPosition);
        return randomPosition;
    }

    private void SpawnBot()
    {
        SpawnEnemy(EnemyPooling.EnemyType.Car);
    }

    private void HandleSpawnTimers()
    {
        planeSpawnTimer += Time.deltaTime;
        carSpawnTimer += Time.deltaTime;
        vanSpawnTimer += Time.deltaTime;
        truckSpawnTimer += Time.deltaTime;
        tankSpawnTimer += Time.deltaTime;
        if (planeSpawnTimer >= 75f)
        {
            SpawnEnemy(EnemyPooling.EnemyType.Planes);
            planeSpawnTimer = 0f;
        }
        if (carSpawnTimer >= 15f)
        {
            SpawnEnemy(EnemyPooling.EnemyType.Car);
            carSpawnTimer = 0f;
        }
        if (vanSpawnTimer >= 45f)
        {
            SpawnEnemy(EnemyPooling.EnemyType.Van);
            vanSpawnTimer = 0f;
        }
        if (truckSpawnTimer >= 50f)
        {
            SpawnEnemy(EnemyPooling.EnemyType.Truck);
            truckSpawnTimer = 0f;
        }
        if (tankSpawnTimer >= 100f)
        {
            SpawnEnemy(EnemyPooling.EnemyType.Tank);
            tankSpawnTimer = 0f;
        }
    }

    private void SpawnEnemy(EnemyPooling.EnemyType enemyType)
    {
        GameObject enemy = EnemyPooling.Instance.GetObject(enemyType);
        if (enemy != null)
        {
            enemy.transform.position = GetValidPosition();
            SpawnedEnemies.Add(enemy);
            spawnedBotNumber++;
        }
    }

    private void RelocateDistantEnemies()
    {
        foreach (GameObject enemy in SpawnedEnemies)
        {
            if (enemy.activeInHierarchy && Vector3.Distance(player.transform.position, enemy.transform.position) > 100f && !enemy.GetComponent<Enemy>().IsDead)
            {
                enemy.transform.position = player.transform.position + (Vector3)(UnityEngine.Random.insideUnitCircle.normalized * 50f);
            }
        }
    }
}
