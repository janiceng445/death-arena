using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractBed : MonoBehaviour
{
    public float Amount;
    public bool TypeDrink = false;
    public int cooldown = 200;
    public float repercussion = 0.25f;
    private int counter = 0;
    bool caffeinated = false;
    bool disabled = false;
    bool repercussed = false;

    public void RestoreEnergy() {
        if (Amount > GameData.TirednessLvl) {
            GameData.TirednessLvl = 0;
        }
        else 
            GameData.TirednessLvl -= Amount;
    }

    public void Caffeine() {
        if (!disabled) {
            GameData.isTired = false;
            caffeinated = true;
            disabled = true;
            repercussed = true;
        }
    }

    void Update() {
        // Caffeine
        if (TypeDrink) {
            if (caffeinated) {
                counter--;
                if (counter <= 0) {
                    counter = 0;
                    caffeinated = false;
                }
            }
            else {
                counter = cooldown;
                GameData.isTired = true;
                disabled = false;

                // Repercussions
                if (repercussed) {
                    GameData.TirednessLvl += repercussion;
                    repercussed = false;
                }
            }
        }
    }
}
