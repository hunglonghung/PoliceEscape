using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
public class BulletFiring : MonoBehaviour
{
    [SerializeField] private float distance = 100f;
    [SerializeField] private float travelSpeed = 0.05f;
    private Vector3 startPosition;
    public void OnInit()
    {
        startPosition = transform.position;
        
    }
    private void Update()
    {
        Debug.Log(Vector3.Distance(startPosition, transform.position));
        transform.Translate(travelSpeed,0,0);
        if (Vector3.Distance(startPosition, transform.position) >= distance)
        {
            Debug.Log("return");
            BulletSpawnner.Instance.ReturnBullet(gameObject);
        }
    }
}