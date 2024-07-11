using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
public class BulletFiring : MonoBehaviour
{
    [SerializeField] private float travelSpeed = 0.05f;    
    public enum BulletType
    {
        Plane,
        Truck,
        Tank
    }
    [SerializeField] public BulletType bulletType;
    private Vector3 startPosition;
    [SerializeField] public float time = 0f ;
    [SerializeField] public float bulletDuration = 5f;
    [SerializeField] public Transform target;
    public void OnInit()
    {
        startPosition = transform.position;
        time = 0f;
        
    }
    private void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector3.MoveTowards(startPosition,target.transform.position, travelSpeed);
        if(time >= bulletDuration)
        {
            BulletPooling.Instance.ReturnObject(gameObject,(BulletPooling.BulletType)(int)bulletType);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        BulletPooling.Instance.ReturnObject(gameObject,(BulletPooling.BulletType)(int)bulletType);
    }
}