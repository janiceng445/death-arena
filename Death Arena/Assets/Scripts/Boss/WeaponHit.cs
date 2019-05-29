using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    public GameObject parent;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            parent.GetComponent<Boss>().Attack();
        }
    }
}
