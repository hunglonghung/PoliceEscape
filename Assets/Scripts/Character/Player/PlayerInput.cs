using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController playerController; 
    [SerializeField] Player player;
    void Start()
    {
        player = GetComponent<Player>();
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if(!player.IsDead)
        {
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
        }
        playerController.SetInputVector(inputVector);


    }

}