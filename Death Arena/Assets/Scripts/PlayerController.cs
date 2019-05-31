using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Statistics
    [SerializeField] public float WalkSpeed = 35f;
    [SerializeField] public float RunSpeed = 45f;

    // Conditions
    private bool isRunning = false;
    private bool isAttacking = false;
    private bool isMoving = false;
    private bool isRolling = false;

    // Variables
    private PlayerManager controller;
    float horizontalMove, verticalMove = 0f;
    private Animator animator;
    public BoxCollider2D weaponCollider;

    void Start() {
        controller = gameObject.GetComponent<PlayerManager>();
        WalkSpeed = PlayerStats.w_speed;
        RunSpeed = PlayerStats.r_speed;
        animator = gameObject.GetComponentInChildren<Animator>();
        //animator = gameObject.GetComponent<Animator>();

        // TEMP
        if (WalkSpeed == 0) {
            WalkSpeed = 35f;
        }
        if (RunSpeed == 0) {
            RunSpeed = 45f;
        }
    }

    void Update() {
        // Check conditions
        CheckConditions();
        // Check animations
        UpdateAnimations();

        // Setting speed
        float speed = 0f;
        if (!isAttacking) {
            speed = !isRunning ? WalkSpeed : RunSpeed;
        }

        // Set move values
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed; 
        verticalMove = Input.GetAxisRaw("Vertical") * speed; 
        isMoving = (Mathf.Abs(horizontalMove) > 0 || Mathf.Abs(verticalMove) > 0) ? true : false;

        // Check if pausing
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameSettings.paused = !GameSettings.paused;
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking) {
            isAttacking = true;
        }

        // Rolling
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift)) {
            Debug.Log("roll");
        }
    }

    void UpdateAnimations() {
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isMoving", isMoving);
        //animator.SetBool("isRolling", isRolling);
    }

    void FixedUpdate ()
    {
        if (!GameSettings.paused) {
            // Move character
            controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime); 
        }
        else {
            controller.Move(0, 0); 
        }
    }


    // Animation functions only
    public void EnableAttackCollision() {
        weaponCollider.enabled = true;
    }
    public void FinishAttacking() {
        isAttacking = false;
        weaponCollider.enabled = false;
    }

}
