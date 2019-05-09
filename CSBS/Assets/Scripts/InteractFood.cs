using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractFood : MonoBehaviour
{
    float amount = 0;
    bool enable = false;
    public Image circle;

    void OnMouseEnter() {
        Debug.Log("Food");
        enable = true;
    }
    
    void OnMouseExit() {
        enable = false;
    }

    void Update() {
        if (enable && Input.GetMouseButton(0) && amount <= 1.0f) {
            amount += 0.005f;
            circle.fillAmount += 0.005f;

            // Level 4 - cuisine
            if (amount >= 0.9f)
                Debug.Log("Eating cuisine");
            // Level 3 - meal
            else if (amount > 0.6f)
                Debug.Log("Eating meal");
            // Level 2 - junk
            else if (amount > 0.3f)
                Debug.Log("Eating junk");
            // Level 1 - snack
            else
                Debug.Log("Eating snack");
        }
    }
    void OnMouseUp() {
        if (amount >= 1.0f) amount = 1.0f; 
        circle.fillAmount = 0;

        // Level 4 - cuisine
        if (amount >= 1.0f)
            Debug.Log("Eating cuisine");
        // Level 3 - meal
        else if (amount > 0.6f)
            Debug.Log("Eating meal");
        // Level 2 - junk
        else if (amount > 0.3f)
            Debug.Log("Eating junk");
        // Level 1 - snack
        else
            Debug.Log("Eating snack");

        if (amount > GameData.HungerLvl)
            GameData.HungerLvl = 0;
        else GameData.HungerLvl -= amount;
        amount = 0;
    }
}
