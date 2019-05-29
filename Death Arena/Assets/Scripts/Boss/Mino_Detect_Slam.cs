using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino_Detect_Slam : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            this.transform.parent.GetComponent<Minotaur>().slamRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Player") {
            this.transform.parent.GetComponent<Minotaur>().slamRange = false;
        }
    }
}
