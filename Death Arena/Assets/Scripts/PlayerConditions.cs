using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerConditions : MonoBehaviour
{
    public int health;
    public GameObject hitbox;
    private int timer;

    // GUI
    public GameObject healthBar;

    void Start() {
        if (PlayerStats.hp == 0) 
            PlayerStats.hp = 50;
        health = PlayerStats.hp;
        timer = 200;
    }

    void Update() {
        Vector3 temp = healthBar.transform.localScale;
        temp.x = (float) health / (float) PlayerStats.hp;
        healthBar.transform.localScale = temp;

        CheckHealth();
    }

    void CheckHealth() {
        if (health <= 0) {
            // Orientation fix
            GameObject boss = GameObject.FindGameObjectWithTag("Enemy");
            if (boss.transform.position.x < transform.position.x && transform.localScale.x < 0) {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
            else if (boss.transform.position.x > transform.position.x && transform.localScale.x > 0) {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }

            // Disable things
            hitbox.GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInChildren<Animator>().Play("Death");
            GetComponentInChildren<PlayerController>().WalkSpeed = 0;

            // Timer until going back to title screen
            timer--;
            if (timer <= 0) {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
