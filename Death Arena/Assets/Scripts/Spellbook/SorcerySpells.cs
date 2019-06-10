using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcerySpells : MonoBehaviour
{
    private GameObject selector;

    void Start() {
        selector = GameObject.Find("Sorce_Selector");
        selector.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnMouseOver() {
        if (Input.GetMouseButton(0)) {
            selector.transform.position = this.transform.position;
            selector.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
