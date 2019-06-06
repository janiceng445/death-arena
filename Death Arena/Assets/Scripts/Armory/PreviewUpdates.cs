using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewUpdates : MonoBehaviour
{
    // Before text
    private Text health;
    private Text energy;
    private Text attack;
    private Text defense;
    private Text money;

    private Item script;

    void Start () {
        health = GameObject.Find("Health").GetComponent<Text>();
        energy = GameObject.Find("Energy").GetComponent<Text>();
        attack = GameObject.Find("Attack").GetComponent<Text>();
        defense = GameObject.Find("Defense").GetComponent<Text>();
        money = GameObject.Find("Money").GetComponent<Text>();
    }

    void OnMouseOver() {
        script = GetComponent<Item>();
        if (!script.isBought) {
            // Stats text
            if (script.healthBuff != 0) {
                int total = PlayerStats.hp - PlayerStats.hp_bon - PlayerStats.hp_bon2 + script.healthBuff;
                int bonus = PlayerStats.hp_bon + PlayerStats.hp_bon2;
                int change = script.healthBuff - PlayerStats.hp_bon - PlayerStats.hp_bon2;
                health.text = "Health: " + total.ToString() + " (" + GetSign(script.healthBuff, bonus) + change.ToString() + ")";
                ChangeColor(health, Mathf.Sign(change));
            }
            energy.text = "Energy: " + (PlayerStats.energy).ToString();
            if (script.attackBuff != 0) {
                int total = PlayerStats.atk - PlayerStats.atk_bon - PlayerStats.atk_bon2 + script.attackBuff;
                int bonus = PlayerStats.atk_bon + PlayerStats.atk_bon2;
                int change = script.attackBuff - PlayerStats.atk_bon - PlayerStats.atk_bon2;
                attack.text = "Attack: " + total.ToString() + " (" + GetSign(script.attackBuff, bonus) + change.ToString() + ")";
                ChangeColor(attack, Mathf.Sign(change));
            }
            if (script.defBuff != 0) {
                int total = PlayerStats.def - PlayerStats.def_bon - PlayerStats.def_bon2 + script.defBuff;
                int bonus = PlayerStats.def_bon + PlayerStats.def_bon2;
                int change = script.defBuff - PlayerStats.def_bon - PlayerStats.def_bon2;
                defense.text = "Defense: " + total.ToString() + " (" + GetSign(script.defBuff, bonus) + change.ToString() + ")";
                ChangeColor(defense, Mathf.Sign(change));
            }

            // Money Text
            money.text = "Money: " + WorldStats.gold.ToString() + " - " + script.cost.ToString();
            money.text = "Money: " + (WorldStats.gold - script.cost).ToString();
        }
        else {
            // Stats text
            GameObject.Find("Canvas").GetComponent<ArrmoryButtons>().ResetText();
            if (script.healthBuff != 0 && script.healthBuff - PlayerStats.hp_bon - PlayerStats.hp_bon2 != 0) {
                int total = PlayerStats.hp - PlayerStats.hp_bon - PlayerStats.hp_bon2 + script.healthBuff;
                int bonus = PlayerStats.hp_bon + PlayerStats.hp_bon2;
                int change = script.healthBuff - PlayerStats.hp_bon - PlayerStats.hp_bon2;
                health.text = "Health: " + total.ToString() + " (" + GetSign(script.healthBuff, bonus) + change.ToString() + ")";
                ChangeColor(health, Mathf.Sign(change));
            }
            energy.text = "Energy: " + (PlayerStats.energy).ToString();
            if (script.attackBuff != 0 && script.attackBuff - PlayerStats.atk_bon - PlayerStats.atk_bon2 != 0) {
                int total = PlayerStats.atk - PlayerStats.atk_bon - PlayerStats.atk_bon2 + script.attackBuff;
                int bonus = PlayerStats.atk_bon + PlayerStats.atk_bon2;
                int change = script.attackBuff - PlayerStats.atk_bon - PlayerStats.atk_bon2;
                attack.text = "Attack: " + total.ToString() + " (" + GetSign(script.attackBuff, bonus) + change.ToString() + ")";
                ChangeColor(attack, Mathf.Sign(change));
            }
            if (script.defBuff != 0 && script.defBuff - PlayerStats.def_bon - PlayerStats.def_bon2 != 0) {
                int total = PlayerStats.def - PlayerStats.def_bon - PlayerStats.def_bon2 + script.defBuff;
                int bonus = PlayerStats.def_bon + PlayerStats.def_bon2;
                int change = script.defBuff - PlayerStats.def_bon - PlayerStats.def_bon2;
                defense.text = "Defense: " + total.ToString() + " (" + GetSign(script.defBuff, bonus) + change.ToString() + ")";
                ChangeColor(defense, Mathf.Sign(change));
            }
        }
    }

    void OnMouseExit() {
        GameObject.Find("Canvas").GetComponent<ArrmoryButtons>().ResetText();
    }

    string GetSign(int newBuff, int currBuff) {
        return Mathf.Sign(newBuff - currBuff) == 1 ? "+" : "";
    }

    void ChangeColor(Text reference, float sign) {
        Color32 pos = new Color32(0x00, 0xFF, 0x0D, 0xFF);
        Color32 neg = new Color32(0xFF, 0x00, 0x00, 0xFF);
        if (sign == 1f) {
            reference.color = pos;
        }
        else if (sign == -1f) {
            reference.color = neg;
        }
    }
}
