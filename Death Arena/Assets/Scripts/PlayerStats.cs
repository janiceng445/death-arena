using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Data
    public static float w_speed;
    public static float r_speed;
    public static int atk;
    public static int def;
    public static int hp;
    public static int energy;

    // Armor buffs
    public static int atk_bon;
    public static int atk_bon2;
    public static int def_bon;
    public static int def_bon2;
    public static int hp_bon;
    public static int hp_bon2;

    // Armor
    public static bool isWearingArmor;
    public static string[] armorSet;
    public static string armorSetName;

    // Weapon
    public static bool hasNewWeapon;
    public static string weaponName;

    void Start() {
        // The default
        w_speed = 35f;
        r_speed = 45f;
        atk = 50;
        def = 5;
        hp = 100;
        energy = 100;
        isWearingArmor = false;
        hasNewWeapon = false;
        atk_bon = 0;
        atk_bon2 = 0;
        def_bon = 0;
        def_bon2 = 0;
        hp_bon = 0;
        hp_bon2 = 0;
        armorSet = new string[10];
        armorSetName = "null";
        weaponName = "null";

        // Save new player data file if needed
        SaveSystem.SaveNewPlayerData();

        // Load actual player data file
        PlayerData pd = SaveSystem.LoadPlayerData();
        if (pd != null) {
            // Bonuses
            atk_bon = pd.atk_bon;
            atk_bon2 = pd.atk_bon2;
            def_bon = pd.def_bon;
            def_bon = pd.def_bon2;
            hp_bon = pd.hp_bon;
            hp_bon = pd.hp_bon2;

            // Calculate with bonuses
            w_speed = pd.w_speed;
            r_speed = pd.r_speed;
            atk = pd.atk + atk_bon + atk_bon2;
            def = pd.def + def_bon + def_bon2;
            hp = pd.hp + hp_bon + hp_bon2;
            energy = pd.energy;
            isWearingArmor = pd.isWearingArmor;
            hasNewWeapon = pd.hasNewWeapon;
            armorSet = pd.armorSet;
            armorSetName = pd.armorSetName;
            weaponName = pd.weaponName;
        }
    }

    public static void AddBonuses(int a, int h, int d) {
        atk += a - atk_bon;
        hp += h - hp_bon;
        def += d - def_bon;
        atk_bon = a;
        hp_bon = h;
        def_bon = d;
    }

    public static void AddBonusesWeapon(int a, int h, int d) {
        atk += a - atk_bon2;
        hp += h - hp_bon2;
        def += d - def_bon2;
        atk_bon2 = a;
        hp_bon2 = h;
        def_bon2 = d;
    }
    
}
