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
            if (collider.name == "shadowliteHitbox") {
                collider.transform.parent.gameObject.GetComponent<Shadowlites>().DestroyThis(); 
            }
            else
            {
                collider.transform.parent.gameObject.GetComponent<Boss>().TakeDamage(power);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "EnemyHitbox") {
        }
    }
}
