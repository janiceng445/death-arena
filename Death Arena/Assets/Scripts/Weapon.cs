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
            power = 500;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "EnemyHitbox") {
            collider.transform.parent.gameObject.GetComponent<Boss>().TakeDamage(power);
        }
    }
}
