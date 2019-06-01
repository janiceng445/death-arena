using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    public int health;

    // GUI
    public GameObject healthBar;

    void Start() {
        PlayerStats.hp = 50;
        health = PlayerStats.hp;
    }

    void Update() {
        Vector3 temp = healthBar.transform.localScale;
        temp.x = (float) health / (float) PlayerStats.hp;
        healthBar.transform.localScale = temp;

        CheckHealth();
    }

    void CheckHealth() {
        if (health <= 0) {
            Debug.Log("gameover");
        }
    }
}
