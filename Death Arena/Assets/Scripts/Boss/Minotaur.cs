using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Boss
{
    //** Ability Cooldown Timer **//
    private float chooseTimer = 0;
    private int ability; 
    private int boulderTossCounter; 
    private int hammerFistCounter;
    private int rampageCounter; 
    // If above 50% HP, only use abilities 1 & 2, If below 50% HP, only use abilities 2 & 3
    private bool switchAttacks; 

    //** Call Animations and Attacks on or off **//
    private bool midstAbility; 
    private bool hammerFistBool; 
    private bool boulderTossBool;
    private bool isBuffing; 
    public bool isCharging; 

    //** Slam Jumpattack **// 
    public BoxCollider2D slamCollider;
    
    //** HammerFist ability **//
    public CircleCollider2D hammerDmg; 
    public BoxCollider2D hammerSlow; 
    public static bool isDmging; 
    public static bool isSlowing;
    private int abilityDamage = 75; 
    public bool damageOnce; 
    

    //** BoulderToss ability **//
    private GameObject boulderCollider;
    private GameObject boulderPosition;  
    public float speed = 1.0f; 

    //** Rampage Ability **//
    private float rampageSpeed = 30.0f; 
    private Vector3 playerCurrLocation; 
    private Vector3 chargePast = Vector3.zero; 
    public bool isWithinRampageZone; 
    public CircleCollider2D rampageRoar; 

    //** Custom conditions **//
    public bool slamRange;
    private bool isJumpAttacking;
    private bool canMove;

    protected override void Start() {
        base.Start();

        // Set stats
        power = 30;
        health = 1500;
        Speed = 3f;
        breathDuration = 1f;
        moneyAmount = 1000;

        ResetBreathTimer();
        CompleteStats();

        // Turn ability colliders off
        hammerDmg.enabled = false;
        hammerSlow.enabled = false; 
        rampageRoar.enabled = false; 
        rampageRoar.radius = 1f; 

        hammerFistBool = false;
        boulderTossBool = false;
    }

    protected override void Update() {
        // Check death
        if (health <= 0 && !dieOnce) {
            base.Die();
        }

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

        // Only do stuff if player is alive
        if (target.GetComponent<PlayerConditions>().health >= 0.001f) {
            // Move mechanic
            Move();

            // Condition checking
            CheckHammerFistConditions (); 

            // Activate hammerfist collider only if bool is true
            if (hammerFistBool) {
                hammerSlow.enabled = true;
            }
            else { 
                hammerSlow.enabled = false;
            }
            
            // Switch attacks if Minotaur is below 50% health
            if (health >= 0.5f * maxHealth)
            {
                switchAttacks = false; 
            }
            else if (health <= 0.5f * maxHealth)
            {
                switchAttacks = true; 
            }

            //*************************************************//Animations//*************************************************//
            //****************************************************************************************************************//
            animator.SetBool("isMoving", isMoving);
            animator.SetBool("isAttacking", isAttacking);
            animator.SetBool("isTakingBreak", isTakingBreak);
            animator.SetBool("isJumpAttacking", isJumpAttacking); 
            animator.SetBool("isHammerFist", hammerFistBool); 
            animator.SetBool("isBoulderToss", boulderTossBool); 
            animator.SetBool("isBuffing", isBuffing); 
            animator.SetBool("isCharging", isCharging);
            //****************************************************************************************************************//
            //****************************************************************************************************************//

            //*************************************************//Abilities//*************************************************//
            //***************************************************************************************************************//
            
            // Punch attack
            if (inRange && !isAttacking && !isTakingBreak && !slamRange && !midstAbility) {
                isAttacking = true;
            }
            // Body slam attack
            else if (slamRange && !isJumpAttacking && !isTakingBreak && !inRange && !midstAbility) {
                isJumpAttacking = true;
            }

            // If performing any ability, then boss is in the middle of performing an ability
            if (hammerFistBool || boulderTossBool || isBuffing || isCharging)
            {
                midstAbility = true; 
            }
            else if (!hammerFistBool && !boulderTossBool && !isBuffing && !isCharging)
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
                Flip(); 
            }
            // After Rage / Buff Animation is over, charge. Minotaur position is equal to the direction going towards the player
            // and go past. Deactivate roar collider
            if (isCharging)
            {
                transform.parent.transform.position += chargePast * Time.deltaTime; 
                rampageRoar.enabled = false; 
            }
            //***************************************************************************************************************//
            //***************************************************************************************************************//

            //*************************************************//Calculations//*************************************************//
            //******************************************************************************************************************//
            // Random Number / Skill Generator every 4 seconds and must not be in the middle of performing an ability
            if (!midstAbility)
            {
                chooseTimer += Time.deltaTime;
                if (chooseTimer >= 3)
                {
                    if (!switchAttacks)
                    {
                        // Pick number from 1 through  50
                        // Every ability has a counter. If ability has been done twice already, repick a number 
                        // until it has become a different ability. Once it has become a different ability, counters reset
                        // This prevents abilities to be done more than 2 times in a row 
                        do
                        {
                            ability = Random.Range (1, 51);
                            // Debug.Log("Ability Chosen: " + ability); 
                            if (boulderTossCounter >= 2)
                            {
                                if (ability >= 26)
                                {
                                    boulderTossCounter = 0; 
                                }
                            }
                            if (hammerFistCounter >= 2)
                            {
                                if (ability <= 25)
                                {
                                    hammerFistCounter = 0; 
                                }
                            }
                        } while (boulderTossCounter == 2 || hammerFistCounter == 2);
                    }
                    else if (switchAttacks)
                    {
                        // Pick number from 25 through 75
                        do
                        {
                            ability = Random.Range (26, 76);
                            if (hammerFistCounter >= 2)
                            {
                                if (ability >= 51)
                                {
                                    hammerFistCounter = 0; 
                                }
                            }
                            if (rampageCounter >= 2)
                            {
                                if (ability <= 50)
                                {
                                    rampageCounter = 0; 
                                }
                            }
                        } while (hammerFistCounter == 2 || rampageCounter == 2);
                    }
                    // Reset Timer
                    chooseTimer = 0; 
                }
            }
            // Above 50% HP: Number from 1-50.      // Below 50% HP: Number from 25-75.
            // 1-25 = BoulderToss                   // 26-50 = HammerFist
            // 26-50 = HammerFist                   // 51-75 = Rampage

            if (ability >= 1 && ability <= 25 && !midstAbility)
            {
                boulderTossCounter++; 
                // Debug.Log ("Boulder: " + boulderTossCounter);
                if (boulderTossCounter != 3)
                {
                    boulderTossBool = true; 
                }
            }
            else if (ability >= 26 & ability <= 50 && !midstAbility)
            {
                hammerFistCounter++; 
                // Debug.Log ("Hammer : " + hammerFistCounter); 
                if (hammerFistCounter != 3)
                {
                    hammerFistBool = true; 
                }
            }
            else if (ability >= 51 && ability <= 75 && !midstAbility)
            {
                rampageCounter++; 
                if (rampageCounter != 3)
                {
                    Flip (); 
                    // If rampage ability is chosen, begin with channeling Roar. Buffing's last frame activates charging. 
                    rampageRoar.enabled = true; 
                    gameObject.transform.parent.GetComponent<Rigidbody2D>().isKinematic = true;
                    isBuffing = true;
                }
                else {
                    gameObject.transform.parent.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }
        }
        else {
            isJumpAttacking = false;
            isMoving = false;
            isTakingBreak = false;
            isAttacking = false;
            hammerFistBool = false;
            boulderTossBool = false;
            isBuffing = false;
        }

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
        // hammerDmg.enabled = true; 
        // hammerSlow.enabled = true; 
        // Changed to be via animation ^^ // 
    }

    void CheckHammerFistConditions ()
    {
        if (isDmging && !damageOnce)
        {
            dealDamage();
            damageOnce = true; 
        }
        else if (isSlowing)
        {
            target.GetComponent<PlayerManager>().isSlowed = true; 
        }
        else if (!isSlowing || !isDmging)
        {
            target.GetComponent<PlayerManager>().isSlowed = false; 
            damageOnce = false; 
        }
    }

    void hammerFistDisabled ()
    {
        hammerFistBool = false; 
        midstAbility = false; 
        hammerDmg.enabled = false;
        hammerSlow.enabled = false;
    }
    //***************************************************************************************************************//
    //***************************************************************************************************************//
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
    //***************************************************************************************************************//
    //***************************************************************************************************************//
    #endregion
    //*************************************************//Rampage//*************************************************//
    //*************************************************************************************************************//
    #region Rampage
    void rampage ()
    {
        isBuffing = false;
        // Player's current location is equal to Minotaur's target location
        playerCurrLocation = targetLocation.position; 
        // Define chargePast to be the direction (target location from minotaur location) 
        chargePast = (playerCurrLocation - transform.parent.transform.position).normalized * rampageSpeed; 
        isCharging = true; 
        Flip (); 
    }
    //***************************************************************************************************************//
    //***************************************************************************************************************//
    #endregion

    void Flip ()
    {
        // Facing left, player is on right
        if (!FacingRight && targetLocation.position.x > transform.parent.transform.position.x) {
            base.Flip();
        }
        // Facing right, player is on left
        else if (FacingRight && targetLocation.position.x < transform.parent.transform.position.x) {
            base.Flip();
        }
    }

    public void ActivateSlamCollision() {
        slamCollider.enabled = true;
    }

    public void DeactivateSlamCollision() {
        slamCollider.enabled = false;
        isTakingBreak = true;
    }

    void dealDamage ()
    {
        float calc_power = abilityDamage - ((float) PlayerStats.def / 2f);
        if (target.GetComponent<PlayerConditions>().health - calc_power <= 0) {
            target.GetComponent<PlayerConditions>().health = 0;
        }
        else {
            target.GetComponent<PlayerConditions>().health -= (int) Mathf.Ceil(calc_power);
        }
    }
}
