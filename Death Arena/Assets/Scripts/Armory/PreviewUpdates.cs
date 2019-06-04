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

    // Before stats
    private int curr_hp;
    private int curr_energy;
    private int curr_atk;
    private int curr_def;
    private int curr_money;

    private ArmorSet script;

    void Start () {
        health = GameObject.Find("Health").GetComponent<Text>();
        energy = GameObject.Find("Energy").GetComponent<Text>();
        attack = GameObject.Find("Attack").GetComponent<Text>();
        defense = GameObject.Find("Defense").GetComponent<Text>();
        money = GameObject.Find("Money").GetComponent<Text>();
        curr_hp = PlayerStats.hp;
        curr_energy = PlayerStats.energy;
        curr_atk = PlayerStats.atk;
        curr_def = PlayerStats.def;
        curr_money = WorldStats.gold;
    }

    void OnMouseOver() {
        script = GetComponent<ArmorSet>();
        if (!script.isBought) {
            // Stats text
            if (script.healthBuff != 0)
                health.text = "Health: " + PlayerStats.hp.ToString() + " + " + script.healthBuff.ToString();
            energy.text = "Energy: " + (PlayerStats.energy).ToString();
            if (script.attackBuff != 0)
                attack.text = "Attack: " + PlayerStats.atk.ToString() + " + " + script.attackBuff.ToString();
            if (script.defBuff != 0)
                defense.text = "Defense: " + PlayerStats.def.ToString() + " + " + script.defBuff.ToString();

            // Money Text
            money.text = "Money: " + WorldStats.gold.ToString() + " - " + script.cost.ToString();
            money.text = "Money: " + (WorldStats.gold - script.cost).ToString();
        }
    }

    void OnMouseExit() {
        health.text = "Health: " + (curr_hp).ToString();
        energy.text = "Energy: " + (curr_energy).ToString();
        attack.text = "Attack: " + (curr_atk).ToString();
        defense.text = "Defense: " + (curr_def).ToString();
        money.text = "Money: " + (curr_money).ToString();
    }
}
