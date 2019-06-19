using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    void Update() {
        gameObject.transform.position = GameObject.Find("Player").transform.position;
    }
}
