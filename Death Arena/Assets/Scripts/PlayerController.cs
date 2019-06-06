using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Statistics
    [SerializeField] public float WalkSpeed = 35f;
    [SerializeField] public float RunSpeed = 45f;
    [SerializeField] public float DashSpeed = 100f;
    

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
    [SerializeField] private Transform plDashEffect;

    // Ability icon references
    private GameObject ability1;

    void Start() {
        controller = gameObject.GetComponent<PlayerManager>();
        WalkSpeed = PlayerStats.w_speed;
        RunSpeed = PlayerStats.r_speed;
        animator = gameObject.GetComponentInChildren<Animator>();
        plDashEffect = Resources.Load<GameObject>("Prefabs/plDashEffect").transform;

        // Ability icon initalization
        ability1 = GameObject.Find("Ability1_overlay");

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

        if (!GameSettings.paused) {
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
        }

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
    }

    void CheckTimers() {
        // Rolling
        if (dashOnce) {
            dashTimer_remaining--;
            ability1.GetComponent<Image>().fillAmount += 0.01f;
            if (ability1.GetComponent<Image>().fillAmount >= 1f) {
                ability1.GetComponent<Image>().fillAmount = 1f;
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

            // Dash character
            if (isMoving && isRunning && !dashOnce && ability1.GetComponent<Image>().fillAmount == 1f) {
                AudioClip dashSFX = (AudioClip) Resources.Load("SFX/dash", typeof(AudioClip));
                GetComponent<AudioSource>().PlayOneShot(dashSFX);
                Vector3 beforeDashPos = transform.position;
                Transform dashEffectTransform = Instantiate(plDashEffect, beforeDashPos, Quaternion.identity);
                dashEffectTransform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(new Vector2(horizontalMove, verticalMove)));
                dashEffectTransform.localScale = new Vector3(DashSpeed / 150f, 1f, 1f);
                dashEffectTransform.GetComponent<SortingGroup>().sortingOrder = gameObject.GetComponent<SortingGroup>().sortingOrder - 1;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove * Time.fixedDeltaTime * DashSpeed, verticalMove * Time.fixedDeltaTime * DashSpeed);
                ability1.GetComponent<Image>().fillAmount = 0;
                dashOnce = true;
            }
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
