using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BulletSpawnner : MonoBehaviour
{
    public static BulletSpawnner Instance;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private List<GameObject> playerBulletPools;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        Instance = this;
        playerBulletPools = new List<GameObject>();
    }
    void Start()
    {
        CreateBulletPool();
    }

    //pool for each weapon type + original size
    public void CreateBulletPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            playerBulletPools.Add(bullet);
        }

    }

    // Set bullet
    public GameObject GetBullet()
    {
        foreach (GameObject bullet in playerBulletPools)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        return null;
    }


    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

}