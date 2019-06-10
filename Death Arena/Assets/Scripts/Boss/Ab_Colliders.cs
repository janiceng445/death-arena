using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Colliders : MonoBehaviour
{
    private PlayerManager playerMovement; 
    private Minotaur rampageRadius; 
    private string suffix;

    private float stunTimer = 0; 
    
    private bool beenStunned = false; 
    private bool initiateStun = true; 

    void Start ()
    {
        suffix = "(Clone)";
        playerMovement = GameObject.Find("Player").GetComponent<PlayerManager>();
        rampageRadius = GetComponentInParent<Minotaur>(); 
    }

    void Update ()
    {
        if (playerMovement.isStunned)
        {
            stunTimer ++;
            if (stunTimer >= 90)
            {
                playerMovement.isStunned = false; 
            }
        }
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // HammerFistZone Collider
            if (this.gameObject.name == "HammerFistZone" + suffix)
            {
                if (!beenStunned && initiateStun)
                {
                    playerMovement.isStunned = true; 
                    beenStunned = true; 
                }
            }
            // Rampage Collider
            if (this.gameObject.name == "RampageOnTrigger") 
            {
                rampageRadius.isWithinRampageZone = false; 
            }
        }
    }

    void OnTriggerStay2D (Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // HammerFistZone Collider
            if (this.gameObject.name == "HammerFistZone" + suffix)
            {
                playerMovement.isSlowed = true;
            }
            // Rampage Collider
            if (this.gameObject.name == "RampageOnTrigger") 
            {
                rampageRadius.isWithinRampageZone = false; 
            }
        }
    }

    void OnTriggerExit2D (Collider2D collider)
    {
        {
            if (collider.gameObject.tag == "Player")
            {
                // HammerFistZone Collider
                if (this.gameObject.name == "HammerFistZone" + suffix)
                {
                    playerMovement.isStunned = false;
                    playerMovement.isSlowed = false; 
                    stunTimer = 0; 
                }
                // Rampage Collider
                if (this.gameObject.name == "RampageOnTrigger") 
                {
                    rampageRadius.isWithinRampageZone = true; 
                }
             }
        }
    }

    // Only stun player when hammerfistzone is first enabled, not when player walks into it
    void OnFirstTrigger ()
    {
        initiateStun = false; 
    }
}
