using System;
using System.Collections.Generic;
using UnityEngine;
public class EnemyPooling : MonoBehaviour
{
    public static EnemyPooling Instance;
    [SerializeField] private int poolSize = 10;
    [SerializeField] public List<GameObject> enemyPrefabs;
    public enum EnemyType
    {
        Car,
        Van,
        Truck,
        Tank,
        Planes
    }
    public EnemyType enemyType;
    private Dictionary<EnemyType, Queue<GameObject>> enemyPoolDictionary = new Dictionary<EnemyType, Queue<GameObject>>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void InitializePool()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(enemyPrefabs[i]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            enemyPoolDictionary.Add((EnemyType)i, objectPool);
        }
    }
    public GameObject GetObject(EnemyType enemyType)
    {
        if (enemyPoolDictionary.ContainsKey(enemyType))
        {
            if (enemyPoolDictionary[enemyType].Count > 0)
            {
                GameObject obj = enemyPoolDictionary[enemyType].Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(enemyPrefabs[(int)enemyType]);
                return obj;
            }
        }
        else
        {
            Debug.LogWarning("Prefab index out of range");
            return null;
        }
    }
    public void ReturnObject(GameObject obj, EnemyType enemyType)
    {
        obj.SetActive(false);
        if (enemyPoolDictionary.ContainsKey(enemyType))
        {
            enemyPoolDictionary[enemyType].Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}