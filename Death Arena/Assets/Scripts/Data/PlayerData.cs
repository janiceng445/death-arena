using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerData
{
    public float w_speed;
    public float r_speed;
    public int atk;
    public int def;
    public int hp;
    public int energy;

    // Armor
    public bool isWearingArmor;

    public PlayerData() {
        w_speed = PlayerStats.w_speed;
        r_speed = PlayerStats.w_speed;
        atk = PlayerStats.atk;
        def = PlayerStats.def;
        hp = PlayerStats.hp;
        energy = PlayerStats.energy;
        isWearingArmor = PlayerStats.isWearingArmor;
    }
}
