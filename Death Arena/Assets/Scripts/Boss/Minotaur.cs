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

    // Enable colliders for respectful abilities
    public BoxCollider2D hammerFistRadius; 
    public BoxCollider2D slamCollider;

    // Collider disabling after time
    private float hammerZone; 

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
        breathDuration = 50;

        ResetBreathTimer();
        CompleteStats();

        hammerFistRadius.enabled = false; 
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
            breathTimer--;
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
        else if (slamRange && !isJumpAttacking && !isTakingBreak && !inRange) {
            isJumpAttacking = true;
        }

        // If performing any ability, then boss is in the middle of performing an ability
        if (hammerFistBool || boulderTossBool || rampageBool)
        {
            midstAbility = true; 
        }
        else
        {
            midstAbility = false; 
            ability = 0;
            hammerZone = 0;
        }

        // Stop moving if attacking
        if (isAttacking || isJumpAttacking) {
            canMove = false;
            isMoving = false;
        }
        else {
            canMove = true;
        }
        //***************************************************************************************************************//
        //***************************************************************************************************************//


        //*************************************************//Calculations//*************************************************//
        //******************************************************************************************************************//
        // Random Number / Skill Generator every _ seconds and must not be in the middle of performing an ability
        chooseTimer ++;
        if (chooseTimer >= 500 && !midstAbility)
        {
            //ability = 1;//Random.Range (1,4); 
            chooseTimer = 0; 
            Debug.Log (ability); 
        }

        // Disable Collider Zones
        if (hammerFistRadius.enabled)
        {
            hammerZone ++;
            Debug.Log(hammerZone);
            if (hammerZone >= 50)
            {
                hammerFistRadius.enabled = false;
                Debug.Log ("false");
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

            if (canMove) {
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

    void hammerFist ()
    {
        hammerFistRadius.enabled = true; 
        hammerFistBool = false; 
        Debug.Log ("hammerfist"); 
    }

    void boulderToss ()
    {
        boulderTossBool = false; 
        Debug.Log ("bouldertoss"); 
    }

    void rampage ()
    {
        rampageBool = false; 
        Debug.Log ("rampage"); 
    }

    public void ActivateSlamCollision() {
        slamCollider.enabled = true;
    }

    public void DeactivateSlamCollision() {
        slamCollider.enabled = false;
        isTakingBreak = true;
    }
}
