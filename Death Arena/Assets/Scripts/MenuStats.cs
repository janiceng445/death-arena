using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStats : MonoBehaviour
{
    // Player stats
    public Text health;
    public Text energy;
    public Text attack;
    public Text defense;

    // World stats
    public Text level;
    public Text gold;
    public Text crystals;

    void Update() {
        ReloadScene();
    }

    public void ReloadScene() {
        // Update player stats
        health.text = "Health: " + PlayerStats.hp;
        energy.text = "Energy: " + PlayerStats.energy;
        attack.text = "Attack: " + PlayerStats.atk;
        defense.text = "Defense: " + PlayerStats.def;

        // Update world stats
        level.text = "Level: " + WorldStats.level;
        gold.text = WorldStats.gold.ToString("n0");
        crystals.text = WorldStats.knowledge_crystals.ToString();
    }
}
