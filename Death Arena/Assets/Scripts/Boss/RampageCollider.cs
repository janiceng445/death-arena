using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampageCollider : MonoBehaviour
{
    private PlayerManager playerMovement; 
    private Minotaur rampageRadius; 
    private string suffix;

    private bool isDmg;
    private bool isSlow; 

    void Start ()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerManager>();
        rampageRadius = GetComponentInParent<Minotaur>(); 
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
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
                // Rampage Collider
                if (this.gameObject.name == "RampageOnTrigger") 
                {
                    rampageRadius.isWithinRampageZone = true; 
                }
             }
        }
    }
}
