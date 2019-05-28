using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre_weapon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            Debug.Log("Hurt player");
        }
    }
}
