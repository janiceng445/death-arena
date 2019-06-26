using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowPlayerCollision : MonoBehaviour
{
    private Paratoria Paratoria;
    private Shadowlites Shadowlites; 
    private PlayerConditions player;
    private int damage = 20; 

    void Start ()
    {
        Paratoria = GameObject.Find("Wraith(Clone)").transform.GetChild(0).GetComponent<Paratoria>(); 
        Shadowlites = GetComponentInParent<Shadowlites>();
        player = GameObject.Find("Player").GetComponent<PlayerConditions>();
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.name == "Player" && Paratoria.NightmareAbility)
        {
            float calc_power = damage - ((float) PlayerStats.def / 2f);
            if (player.health - calc_power <= 0) {
                player.health = 0;
            }
            else {
                player.health -= (int) Mathf.Ceil(calc_power);
            }
            Paratoria.Heal(20); 
            Shadowlites.DestroyThis(); 
        }
    }
}
