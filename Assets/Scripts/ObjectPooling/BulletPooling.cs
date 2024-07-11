using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling Instance;
    [SerializeField] private int poolSize = 5;
    [SerializeField] public enum BulletType
    {
        Plane,
        Truck,
        Tank
    }
    [SerializeField] private List<GameObject> bulletPrefabs;
    [SerializeField] private Dictionary<BulletType,Queue<GameObject>> bulletPoolDictionary;

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

    //pool for each weapon type + original size
    public void InitializePool()
    {
        for (int i = 0; i < bulletPrefabs.Count; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(bulletPrefabs[i]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            bulletPoolDictionary.Add((BulletType)i, objectPool);
        }

    }

    public GameObject GetObject(BulletType bulletType)
    {
        if (bulletPoolDictionary.ContainsKey(bulletType))
        {
            if (bulletPoolDictionary[bulletType].Count > 0)
            {
                GameObject obj = bulletPoolDictionary[bulletType].Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(bulletPrefabs[(int)bulletType]);
                return obj;
            }
        }
        else
        {
            Debug.LogWarning("Prefab index out of range");
            return null;
        }
    }
    public void ReturnObject(GameObject obj, BulletType bulletType)
    {
        obj.SetActive(false);
        if (bulletPoolDictionary.ContainsKey(bulletType))
        {
            bulletPoolDictionary[bulletType].Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }

}