using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekTarget : MonoBehaviour
{
    private Vector3 target;
    private float speed;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Enemy").transform.position;
        speed = 0.3f;
        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    void Update() {
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}
