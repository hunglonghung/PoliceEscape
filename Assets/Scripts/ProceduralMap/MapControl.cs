using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public static MapControl Instance;
    [SerializeField] public int maxSpawnedMaps = 2; 
    public List<GameObject> spawnedMaps = new List<GameObject>();
    private List<int> spawnedMapIndices = new List<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnMap(GameObject newMap, int prefabIndex)
    {
        spawnedMaps.Add(newMap);
        spawnedMapIndices.Add(prefabIndex);
        if (spawnedMaps.Count > maxSpawnedMaps)
        {
            GameObject oldMap = spawnedMaps[0];
            int oldMapIndex = spawnedMapIndices[0];
            spawnedMaps.RemoveAt(0);
            spawnedMapIndices.RemoveAt(0);
            ObjectPool.Instance.ReturnObject(oldMap, oldMapIndex);
        }
    }
}
