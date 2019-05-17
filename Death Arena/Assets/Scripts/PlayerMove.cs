﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Statistics
    [SerializeField] public float WalkSpeed = 35f;
    [SerializeField] public float RunSpeed = 45f;

    // Conditions
    private bool isRunning = false;
    private bool isAttacking = false;

    // Variables
    private PlayerController controller;
    float horizontalMove, verticalMove = 0f;

    void Start() {
        controller = gameObject.GetComponent<PlayerController>();
    }

    void Update() {
        // Check conditions
        CheckConditions();

        // Setting speed
        float speed = 0f;
        if (!isAttacking) {
            speed = !isRunning ? WalkSpeed : RunSpeed;
        }

        // Set move values
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed; 
        verticalMove = Input.GetAxisRaw("Vertical") * speed; 
    }

    void CheckConditions() {
        // Running
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            isRunning = false;
        }

        // Attacking
        if (Input.GetKeyDown(KeyCode.Space)) {
            isAttacking = true;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            isAttacking = false;
        }

    }

    void FixedUpdate ()
    {
        // Move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime); 
    }

}
