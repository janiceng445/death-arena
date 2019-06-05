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

    public int atk_bon;
    public int atk_bon2;
    public int def_bon;
    public int def_bon2;
    public int hp_bon;
    public int hp_bon2;

    // Armor
    public bool isWearingArmor;
    public bool hasNewWeapon;
    public string[] armorSet;
    public string armorSetName;
    public string weaponName;

    public PlayerData() {
        w_speed = PlayerStats.w_speed;
        r_speed = PlayerStats.w_speed;
        atk = PlayerStats.atk - PlayerStats.atk_bon - PlayerStats.atk_bon2;
        def = PlayerStats.def - PlayerStats.def_bon - PlayerStats.def_bon2;
        hp = PlayerStats.hp - PlayerStats.hp_bon - PlayerStats.hp_bon2;
        energy = PlayerStats.energy;
        isWearingArmor = PlayerStats.isWearingArmor;
        hasNewWeapon = PlayerStats.hasNewWeapon;
        atk_bon = PlayerStats.atk_bon;
        atk_bon2 = PlayerStats.atk_bon2;
        def_bon = PlayerStats.def_bon;
        def_bon2 = PlayerStats.def_bon2;
        hp_bon = PlayerStats.hp_bon;
        hp_bon2 = PlayerStats.hp_bon2;
        armorSet = PlayerStats.armorSet;
        armorSetName = PlayerStats.armorSetName;
        weaponName = PlayerStats.weaponName;
    }
}
