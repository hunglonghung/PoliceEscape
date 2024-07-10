using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField] private List<GameObject> mapPrefabs;
    [SerializeField] private int poolSize = 10;
    private Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

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
        for (int i = 0; i < mapPrefabs.Count; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(mapPrefabs[i]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(i, objectPool);
        }
    }

    public GameObject GetObject(int prefabIndex)
    {
        if (poolDictionary.ContainsKey(prefabIndex))
        {
            if (poolDictionary[prefabIndex].Count > 0)
            {
                GameObject obj = poolDictionary[prefabIndex].Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(mapPrefabs[prefabIndex]);
                return obj;
            }
        }
        else
        {
            Debug.LogWarning("Prefab index out of range");
            return null;
        }
    }

    public void ReturnObject(GameObject obj, int prefabIndex)
    {
        obj.SetActive(false);
        if (poolDictionary.ContainsKey(prefabIndex))
        {
            poolDictionary[prefabIndex].Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}
