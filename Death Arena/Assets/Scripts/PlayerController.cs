using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Statistics
    [SerializeField] public float WalkSpeed = 35f;
    [SerializeField] public float RunSpeed = 45f;
    [SerializeField] public float DashSpeed = 5f;
    

    // Conditions
    private bool isRunning = false;
    private bool isAttacking = false;
    private bool isMoving = false;
    private bool isDashing = false;

    // Once conditions
    private bool dashOnce = false;

    // Cooldown timers
    private int dashTimer;
    private int dashTimer_remaining;

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

        // Timer initializations
        dashTimer = 100;
        dashTimer_remaining = dashTimer;

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
        // Check timers
        CheckTimers();

        // Setting speed
        float speed = 0f;
        if (!isAttacking) {
            speed = WalkSpeed;
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
        if (isMoving && isRunning && !dashOnce && dashTimer_remaining == dashTimer) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove * Time.fixedDeltaTime * 5f, verticalMove * Time.fixedDeltaTime * 5f) * DashSpeed;
            dashOnce = true;
        }
    }

    void CheckTimers() {
        // Rolling
        if (dashOnce) {
            dashTimer_remaining--;
            if (dashTimer_remaining <= 0) {
                dashTimer_remaining = dashTimer;
                dashOnce = false;
            }
        }
    }

    void UpdateAnimations() {
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isDashing", isDashing);
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
    public void DisableRolling() {
        isDashing = false;
    }
}
