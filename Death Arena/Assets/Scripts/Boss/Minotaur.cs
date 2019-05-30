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
    private GameObject zoneObject;
    private CircleCollider2D hammerZone;  
    private float hammerAbilityTimer; 

    protected override void Start() {
        base.Start();

        // Set stats
        power = 50;
        health = 500;
        Speed = 3f;
        breathDuration = 50;

        weaponCollider = gameObject.transform.Find("bone_1/bone_2/bone_3/weapon").gameObject.GetComponent<BoxCollider2D>();

        ResetBreathTimer();

        // Create gameobject and define hammerZone to be this gameobject with the addition of the collider
        zoneObject = new GameObject ("hammerFistCollider");
        hammerZone = zoneObject.AddComponent<CircleCollider2D>();
        hammerZone.enabled = false; 
        hammerZone.radius = 5; 
        hammerZone.isTrigger = true; 
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
        else if (!hammerFistBool && !boulderTossBool && !rampageBool)
        {
            midstAbility = false; 
            ability = 0;
        }
        
        // Random Number / Skill Generator every _ seconds and must not be in the middle of performing an ability
        if (!midstAbility)
        {
            chooseTimer ++;
            if (chooseTimer >= 250)
            {
                ability = 1;//Random.Range (1,4); 
                chooseTimer = 0; 
            }
        }
            
        // Call Animations 
        animator.SetBool("isHammerFist", hammerFistBool); 
        animator.SetBool("isBoulderToss", boulderTossBool); 
        animator.SetBool("isRampage", rampageBool); 

        // If hammerFist radius is enabled, start a timer for when it should vanish and thus end the hammerFist ability
        if (hammerZone.enabled)
        {
            hammerAbilityTimer ++; 
            Debug.Log (hammerAbilityTimer); 
            if (hammerAbilityTimer >= 300)
            {
                hammerZone.enabled = false; 
                hammerFistBool = false; 
                hammerAbilityTimer = 0; 
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
        // Manipulate hammerFist radius transform position values about where the boss is 
        float radiusX = gameObject.transform.position.x; 
        float radiusY = gameObject.transform.position.y;
        float radiusZ = gameObject.transform.position.z;
        hammerZone.transform.position = new Vector3 (radiusX, radiusY, radiusZ);
        hammerZone.enabled = true; 
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
