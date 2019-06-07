using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enhancement : MonoBehaviour
{
    private ParticleSystem particle;
    private bool isSelected;
    private Vector3 oldScale;
    private Vector3 newScale;

    void Start() {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
        isSelected = false;
        oldScale = new Vector3(30, 30, 30);
        newScale = new Vector3(50, 50, 50);
    }

    void Update() {
        if (isSelected && !particle.isEmitting) {
            particle.Play();
        }
    }

    void OnMouseOver() {
        isSelected = true;
        this.transform.localScale = newScale;
        if (Input.GetMouseButtonDown(0)) {
            Vector3 currScale = gameObject.transform.GetChild(4).transform.localScale;
            currScale.y += 0.1f;
            gameObject.transform.GetChild(4).transform.localScale = currScale;
        }
    }

    void OnMouseExit() {
        isSelected = false;
        particle.Stop();
        this.transform.localScale = oldScale;
    }
}
