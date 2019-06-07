using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Colliders : MonoBehaviour
{
    private PlayerManager playerMovement; 
    private string suffix;

    private float stunTimer = 0; 
    
    private bool beenStunned = false; 
    private bool initiateStun = true; 

    void Start ()
    {
        suffix = "(Clone)";
        playerMovement = GameObject.Find("Player").GetComponent<PlayerManager>();
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
        // HammerFistZone Collider
        if (collider.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "HammerFistZone" + suffix)
            {
                if (!beenStunned && initiateStun)
                {
                    playerMovement.isStunned = true; 
                    beenStunned = true; 
                }
            }
        }
    }

    void OnTriggerStay2D (Collider2D collider)
    {
        // HammerFistZone Collider
        if (collider.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "HammerFistZone" + suffix)
            {
                playerMovement.isSlowed = true;
            }
        }
    }

    void OnTriggerExit2D (Collider2D collider)
    {
        // HammerFistZone Collider
        {
            if (collider.gameObject.tag == "Player")
            {
                if (this.gameObject.name == "HammerFistZone" + suffix)
                {
                    playerMovement.isStunned = false;
                    playerMovement.isSlowed = false; 
                    stunTimer = 0; 
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
