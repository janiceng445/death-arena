using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractFood : MonoBehaviour
{
    float amount = 0;
    bool enable = false;
    bool beginCD = false;
    public bool disabled = false;
    public Image circle;
    public int hungry_cooldown = 0;
    public float sanity_amount = 0.1f;


    void OnMouseOver() {
        enable = true;
    }
    
    void OnMouseExit() {
        Calculate();
    }

    Color orange = new Color(1, .47f, 0);

    void Update() {
        // Hungry level
        if (enable && Input.GetMouseButton(0) && amount <= 1.0f && !disabled) {
            amount += 0.005f;
            circle.fillAmount += 0.005f;

            // Level 5 - cuisine
            if (amount == 1f) {
                circle.color = Color.white;
                hungry_cooldown = 500;
            }
            // Level 4 - feast
            else if (amount >= 0.9f) {
                circle.color = Color.red;
                hungry_cooldown = 200;
            }
            // Level 3 - meal
            else if (amount > 0.6f) {
                circle.color = orange;
                hungry_cooldown = 150;
            }
            // Level 2 - junk
            else if (amount > 0.3f) {
                circle.color = Color.yellow;
                hungry_cooldown = 100;
            }
            // Level 1 - snack
            else {
                circle.color = Color.white;
                hungry_cooldown = 50;
            }
        }

        // Hungry cooldown
        if (GameData.fed) {
            hungry_cooldown--;
            disabled = true;
        }

        if (beginCD && hungry_cooldown < 0) {
            hungry_cooldown = 0;
            GameData.fed = false;
            beginCD = false;
            disabled = false;

            // Sanity
            if (GameData.SanityLvl <= sanity_amount) {
                GameData.SanityLvl = 0;
            }
            else {
                GameData.SanityLvl -= sanity_amount;
            }
        }

        // Sanity check
        if (GameData.SanityLvl <= 0 && GameData.HungerLvl < 0.5f) {
            GameData.SanityLvl = 0;
        }
        else {
            GameData.SanityLvl -= 0.0001f;
        }
    }
    void OnMouseUp() {
        Calculate();
    }

    void Calculate() {
        // Hunger
        GameData.fed = true;
        beginCD = true;
        if (amount >= 1.0f || amount / 2 > GameData.HungerLvl) {
            GameData.HungerLvl = 0;
        }
        else {
            GameData.HungerLvl -= amount / 2;
        }
        amount = 0;
        circle.fillAmount = 0;
        enable = false;
    }
}
