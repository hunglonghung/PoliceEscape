using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController playerController; 
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        playerController.SetInputVector(inputVector);
    }

}