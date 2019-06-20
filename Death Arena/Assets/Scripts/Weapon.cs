using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int power;

    void Start() {
        power = WeaponStats.weapon_power;
        
        // TEMP
        power = PlayerStats.atk;

        if (power == 0) {
            power = 100;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "EnemyHitbox") {
            // If shadowlites
            if (collider.name == "shadowliteHitbox") {
                collider.transform.parent.gameObject.GetComponent<Shadowlites>().DestroyThis(); 
                GameObject.Find("Wraith").transform.GetChild(0).GetComponent<Paratoria>().shadowAttack = true; 
                GameObject.Find("Wraith").transform.GetChild(0).GetComponent<Paratoria>().DealDamage(); 
            }
            else if (collider.transform.parent.transform.parent.name == "Wraith" && !collider.transform.parent.gameObject.GetComponent<Paratoria>().canMove) {
                // If attacking Paratoria only when not moving 
                collider.transform.parent.gameObject.GetComponent<Paratoria>().DealDamage(); 
            }
            else {
                // So long as it's not Paratoria (due to moving / not moving purposes), deal damage to boss 
                if (collider.transform.parent.transform.parent.name != "Wraith") {
                    collider.transform.parent.gameObject.GetComponent<Boss>().TakeDamage(power);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "EnemyHitbox") {
            // If you are not attacking shadowlite or anything essentially, you are not attacking shadowlites then (bool)
            GameObject.Find("Wraith").transform.GetChild(0).GetComponent<Paratoria>().shadowAttack = false; 
        }
    }
}
