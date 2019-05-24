using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int power;

    void Start() {
        power = WeaponStats.weapon_power;

        // TEMP
        power = 10;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "EnemyHitbox") {
            collider.gameObject.transform.parent.GetComponent<EnemyController>().TakeDamage(power);
        }
    }
}
