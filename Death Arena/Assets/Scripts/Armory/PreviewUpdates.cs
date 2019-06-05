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

    private ArmorSet script;

    void Start () {
        health = GameObject.Find("Health").GetComponent<Text>();
        energy = GameObject.Find("Energy").GetComponent<Text>();
        attack = GameObject.Find("Attack").GetComponent<Text>();
        defense = GameObject.Find("Defense").GetComponent<Text>();
        money = GameObject.Find("Money").GetComponent<Text>();
    }

    void OnMouseOver() {
        script = GetComponent<ArmorSet>();
        if (!script.isBought) {
            // Stats text
            if (script.healthBuff != 0)
                health.text = "Health: " + (PlayerStats.hp - PlayerStats.hp_bon + script.healthBuff).ToString() + " (" + GetSign(script.healthBuff, PlayerStats.hp_bon) + (script.healthBuff - PlayerStats.hp_bon).ToString() + ")";
            energy.text = "Energy: " + (PlayerStats.energy).ToString();
            if (script.attackBuff != 0)
                attack.text = "Attack: " + (PlayerStats.atk - PlayerStats.atk_bon + script.attackBuff).ToString() + " (" + GetSign(script.attackBuff, PlayerStats.atk_bon) + (script.attackBuff - PlayerStats.atk_bon).ToString() + ")";
            if (script.defBuff != 0)
                defense.text = "Defense: " + (PlayerStats.def - PlayerStats.def_bon + script.defBuff).ToString() + " (" + GetSign(script.defBuff, PlayerStats.def_bon) + (script.defBuff - PlayerStats.def_bon).ToString() + ")";

            // Money Text
            money.text = "Money: " + WorldStats.gold.ToString() + " - " + script.cost.ToString();
            money.text = "Money: " + (WorldStats.gold - script.cost).ToString();
        }
    }

    void OnMouseExit() {
        health.text = "Health: " + (PlayerStats.hp).ToString();
        energy.text = "Energy: " + (PlayerStats.energy).ToString();
        attack.text = "Attack: " + (PlayerStats.atk).ToString();
        defense.text = "Defense: " + (PlayerStats.def).ToString();
        money.text = "Money: " + (WorldStats.gold).ToString();
    }

    string GetSign(int newBuff, int currBuff) {
        return Mathf.Sign(newBuff - currBuff) == 1 ? "+" : "";
    }
}
