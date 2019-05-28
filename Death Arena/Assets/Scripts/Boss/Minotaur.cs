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
    public Collider2D hammerFistRadius; 

    // Collider disabling after time
    private float hammerZone; 

    protected override void Start() {
        base.Start();

        // Set stats
        power = 50;
        health = 500;
        Speed = 3f;
        breathDuration = 50;

        weaponCollider = gameObject.transform.Find("bone_1/bone_2/bone_3/weapon").gameObject.GetComponent<BoxCollider2D>();

        ResetBreathTimer();

        hammerFistRadius = GetComponentInChildren<CircleCollider2D>(); 
        hammerFistRadius.enabled = false; 
    }

    protected override void Update() {
        base.Update();
        base.Move();
        base.Die();

        if (inRange && !isAttacking && !isTakingBreak) {
            isAttacking = true;
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
        
        // Random Number / Skill Generator every _ seconds and must not be in the middle of performing an ability
        chooseTimer ++;
        if (chooseTimer >= 500 && !midstAbility)
        {
            ability = 1;//Random.Range (1,4); 
            chooseTimer = 0; 
            Debug.Log (ability); 
        }
            
        // Call Animations 
        animator.SetBool("isHammerFist", hammerFistBool); 
        animator.SetBool("isBoulderToss", boulderTossBool); 
        animator.SetBool("isRampage", rampageBool); 

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
}
