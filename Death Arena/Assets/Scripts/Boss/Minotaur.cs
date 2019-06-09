using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Boss
{
    // Timer between choosing attacks
    private float chooseTimer = 0;
    private int ability; 

    // Call Animations and Attacks on or off 
    private bool midstAbility; 
    private bool hammerFistBool; 
    private bool boulderTossBool;
    private bool rampageBool; 

    // HammerFist ability
    public BoxCollider2D slamCollider;
    // Defined to be instantiated polygon collider gameobject
    private GameObject hammerFistCollider; 

    // BoulderToss ability
    private GameObject boulderCollider;
    private GameObject boulderPosition;  
    public float speed = 1.0f; 

    // Custom conditions
    public bool slamRange;
    private bool isJumpAttacking;
    private bool canMove;

    protected override void Start() {
        base.Start();

        // Set stats
        power = 50;
        health = 500;
        Speed = 3f;
        breathDuration = 1f;
        moneyAmount = 1000;

        ResetBreathTimer();
        CompleteStats();
 
    }

    protected override void Update() {
        // Check death
        if (health <= 0 && !dieOnce) {
            base.Die();
        }

        // Move mechanic
        Move();

        // Cooldown between attacks
        if (isTakingBreak) {
            isAttacking = false;
            isJumpAttacking = false;
            breathTimer -= Time.deltaTime;
            if (breathTimer <= 0) {
                isTakingBreak = false;
                ResetBreathTimer();
            }
        }

        //*************************************************//Abilities//*************************************************//
        //***************************************************************************************************************//
        
        // Punch attack
        if (inRange && !isAttacking && !isTakingBreak && !slamRange) {
            isAttacking = true;
        }
        // Body slam attack
        else if (slamRange && !isJumpAttacking && !isTakingBreak && !inRange && !midstAbility) {
            isJumpAttacking = true;
        }

        // If performing any ability, then boss is in the middle of performing an ability
        if (hammerFistBool || boulderTossBool || rampageBool)
        {
            midstAbility = true; 
        }
        else if (!hammerFistBool && !boulderTossBool && !rampageBool)
        {
            midstAbility = false; 
            ability = 0;
        }

        // Stop moving if attacking
        if (isAttacking || isJumpAttacking) {
            canMove = false;
            isMoving = false;
        }
        else {
            canMove = true;
        }
        
        // Reorientate on boulder toss if player switched sides
        if (boulderTossBool) {
            // Facing left, player is on right
            if (!FacingRight && targetLocation.position.x > transform.position.x) {
                base.Flip();
            }
            // Facing right, player is on left
            else if (FacingRight && targetLocation.position.x < transform.position.x) {
                base.Flip();
            }
        }
        //***************************************************************************************************************//
        //***************************************************************************************************************//


        //*************************************************//Calculations//*************************************************//
        //******************************************************************************************************************//
        // Random Number / Skill Generator every _ seconds and must not be in the middle of performing an ability
        if (!midstAbility)
        {
            chooseTimer += Time.deltaTime;
            if (chooseTimer >= 4)
            {
                ability = 1;//Random.Range (1,4); 
                chooseTimer = 0; 
            }
        }

        // If ability is 1-3 and are not performing the ability at this time, then perform the ability and
        // activate the animation. Inside the animation, the respectful function will be called. Inside the 
        // respectful function, the ability will eventually be declared false and they will no longer be 
        // in the middle of attacking and therefore could generate a new ability. 
        if (ability == 1 && !midstAbility)
        {
            hammerFistBool = true;
        }
        else if (ability == 2 && !midstAbility)
        {
            boulderTossBool = true; 
        }
        else if (ability == 3 && !midstAbility)
        {
            rampageBool = true; 
        }

        //******************************************************************************************************************//
        //******************************************************************************************************************//
        
            
        //*************************************************//Animations//*************************************************//
        //****************************************************************************************************************//
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isTakingBreak", isTakingBreak);
        animator.SetBool("isJumpAttacking", isJumpAttacking); 
        animator.SetBool("isHammerFist", hammerFistBool); 
        animator.SetBool("isBoulderToss", boulderTossBool); 
        animator.SetBool("isRampage", rampageBool); 
        //****************************************************************************************************************//
        //****************************************************************************************************************//
    }

    protected override void Move() {
        if (!GameSettings.paused) {
            // Fix sorting order
            sprite.sortingOrder = Mathf.RoundToInt(transform.parent.transform.position.y * 100f) * -1;

            if (canMove && !midstAbility) {
                isMoving = true;
                // Move
                if (Vector2.Distance(myLocation.position, targetLocation.position) > DistanceAway) {
                    transform.parent.transform.position = Vector2.MoveTowards(transform.parent.transform.position, targetLocation.position, Speed * Time.deltaTime);
                }
                
                // Flip
                if (transform.parent.transform.position.x < targetLocation.position.x && !FacingRight) {
                    base.Flip();
                }
                else if (transform.parent.transform.position.x > targetLocation.position.x && FacingRight) {
                    base.Flip();
                }
            }
            else {
                isMoving = false;
            }
        }
    }

    //*************************************************//HammerFist//*************************************************//
    //****************************************************************************************************************//
    #region Hammerfist Ability
    void hammerFist ()
    {
        // Manipulate hammerFist radius transform position values about where the boss (hand) is 
        float radiusX = gameObject.transform.position.x; 
        if (FacingRight)
        {
            radiusX += 3; 
        }
        else if (!FacingRight)
        {
            radiusX -= 3; 
        }
        float radiusY = gameObject.transform.position.y;
        float radiusZ = gameObject.transform.position.z;
        hammerFistCollider = Instantiate(Resources.Load<GameObject>("Prefabs/HammerFistZone"), new Vector3(radiusX, radiusY - 5, radiusZ), Quaternion.identity);
        hammerFistCollider.GetComponent<PolygonCollider2D>().isTrigger = true; 
    }

    void hammerFistDisabled ()
    {
        hammerFistBool = false; 
        midstAbility = false; 
    }
    #endregion
    //*************************************************//BoulderToss//*************************************************//
    //*****************************************************************************************************************//
    #region Bouldertoss Ability
    void boulderToss ()
    {
        boulderCollider = Instantiate(Resources.Load<GameObject>("Prefabs/Boulder"), boulderGetLocation(), Quaternion.identity); 
        Destroy(boulderCollider, 8.5f); 
    }

    public Vector3 boulderGetLocation ()
    {
        boulderCollider = GameObject.Find("Boulder"); 
        Vector3 boulderPos = boulderCollider.transform.position; 
        return boulderPos; 
    }

    void boulderTossDisabled ()
    {
        boulderTossBool = false; 
    }
    #endregion
    //*************************************************//Rampage//*************************************************//
    //*************************************************************************************************************//
    #region Rampage
    void rampage ()
    {
        rampageBool = false; 
    }
    #endregion

    public void ActivateSlamCollision() {
        slamCollider.enabled = true;
    }

    public void DeactivateSlamCollision() {
        slamCollider.enabled = false;
        isTakingBreak = true;
    }
}
