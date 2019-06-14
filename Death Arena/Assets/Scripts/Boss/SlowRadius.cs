using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRadius : MonoBehaviour
{
    private PlayerManager player;
    private float damage = 0.25f; 
    private bool withinZone; 

    void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>(); 
    }
    void Update ()
    {
        if (withinZone)
        {
            float calc_power = damage - ((float) PlayerStats.def / 2f);
            if (player.GetComponent<PlayerConditions>().health - calc_power <= 0) {
                player.GetComponent<PlayerConditions>().health = 0;
            }
            else {
                player.GetComponent<PlayerConditions>().health -= (int) Mathf.Ceil(calc_power);
            }
        }
    }
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.name == "Player")
        {
            player.isSlowed = true; 
            withinZone = true; 
        }
    }
    void OnTriggerStay2D (Collider2D col)
    {
        if (col.name == "Player")
        {
            player.isSlowed = true;
            withinZone = true;  
        }
    }
    void OnTriggerExit2D (Collider2D col)
    {
        if (col.name == "Player")
        {
            player.isSlowed = false; 
            withinZone = false; 
        }
    }
}
