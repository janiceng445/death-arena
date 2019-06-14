using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampageCollider : MonoBehaviour
{
    private int power = 75; 
    private GameObject target;
    private Minotaur Minotaur; 
    // private PlayerManager playerMovement; 
    // private Minotaur rampageRadius; 
    // private string suffix;

    // private bool isDmg;
    // private bool isSlow; 

    void Start ()
    {
        // playerMovement = GameObject.Find("Player").GetComponent<PlayerManager>();
        // rampageRadius = GetComponentInParent<Minotaur>(); 
        target = GameObject.Find("Player"); 
        Minotaur = GameObject.Find("Minotaur").transform.GetChild(0).GetComponent<Minotaur>(); 
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.name == "Player" && Minotaur.isCharging)
        {
            float calc_power = power - ((float) PlayerStats.def / 2f);
            if (target.GetComponent<PlayerConditions>().health - calc_power <= 0) {
                target.GetComponent<PlayerConditions>().health = 0;
            }
            else {
                target.GetComponent<PlayerConditions>().health -= (int) Mathf.Ceil(calc_power);
            }
        }
    }
}
