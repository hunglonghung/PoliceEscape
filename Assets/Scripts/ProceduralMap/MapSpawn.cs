using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> map;
    [SerializeField] private float topTransformMultiplier = 5f;
    [SerializeField] private float botTransformMultiplier = 5f;
    [SerializeField] private float rightTransformMultiplier = 5f;
    [SerializeField] private float leftTransformMultiplier = 5f;
    [SerializeField] private float checkRadius = 1f; // Bán kính để kiểm tra sự tồn tại của map
    [SerializeField] private Collider2D[] colliders;

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject newMap = null;
        if (other.gameObject.tag == "player")
        {
            int randomMapIndex = Random.Range(0, map.Count);
            Vector3 newPosition = Vector3.zero;
            bool shouldSpawn = false;

            switch(gameObject.tag)
            {
                case "top":
                {
                    float rotationZ = other.transform.eulerAngles.z;
                    newPosition = gameObject.transform.position + topTransformMultiplier * Vector3.up;
                    shouldSpawn = !IsMapPresentAtPosition(newPosition);
                    break;
                }
                case "bot":
                {
                    float rotationZ = other.transform.eulerAngles.z;
                    newPosition = gameObject.transform.position + botTransformMultiplier * Vector3.down;
                    shouldSpawn = !IsMapPresentAtPosition(newPosition);
                    break;
                }
                case "right":
                {
                    float rotationZ = other.transform.eulerAngles.z;
                    newPosition = gameObject.transform.position + rightTransformMultiplier * Vector3.right + 56.5f * Vector3.up;
                    shouldSpawn = !IsMapPresentAtPosition(newPosition);
                    break;
                }
                case "left":
                {
                    float rotationZ = other.transform.eulerAngles.z;
                    newPosition = gameObject.transform.position + leftTransformMultiplier * Vector3.left + 56.5f * Vector3.up;
                    shouldSpawn = !IsMapPresentAtPosition(newPosition);
                    break;
                }
                
            }


            if (shouldSpawn)
            {
                newMap = ObjectPool.Instance.GetObject(randomMapIndex);
                newMap.transform.position = newPosition;
                MapControl.Instance.SpawnMap(newMap, randomMapIndex);
            }
        }
    }

    private bool IsMapPresentAtPosition(Vector3 position)
    {
        colliders = Physics2D.OverlapCircleAll(position, checkRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("map"))
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Vector3 checkPosition = Vector3.zero;

            switch (gameObject.tag)
            {
                case "top":
                    checkPosition = gameObject.transform.position + topTransformMultiplier * Vector3.up;
                    break;
                case "bot":
                    checkPosition = gameObject.transform.position + botTransformMultiplier * Vector3.down;
                    break;
                case "right":
                    checkPosition = gameObject.transform.position + rightTransformMultiplier * Vector3.right;
                    break;
                case "left":
                    checkPosition = gameObject.transform.position + leftTransformMultiplier * Vector3.left;
                    break;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(checkPosition, checkRadius);
        }
    }

}
