using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConditions : MonoBehaviour
{
    public int health;
    public int atkPower;

    void Start() {
        health = 100;
        atkPower = 1;
    }

    void SetHealth(int amount) {
        health = amount;
    }

    void SetAttackPower(int amount) {
        atkPower = amount;
    }

    void Update() {
        if (health <= 0) {
            gameObject.GetComponent<EnemyController>().Die();
        }
    }
}
