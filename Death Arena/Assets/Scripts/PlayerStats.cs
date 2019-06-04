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
    public static int def_bon;
    public static int hp_bon;

    // Armor
    public static bool isWearingArmor;

    void Start() {
        // The default
        w_speed = 35f;
        r_speed = 45f;
        atk = 50;
        def = 5;
        hp = 100;
        energy = 100;
        isWearingArmor = false;
        atk_bon = 0;
        def_bon = 0;
        hp_bon = 0;

        // Save new player data file if needed
        SaveSystem.SaveNewPlayerData();

        // Load actual player data file
        PlayerData pd = SaveSystem.LoadPlayerData();
        if (pd != null) {
            // Bonuses
            atk_bon = pd.atk_bon;
            def_bon = pd.def_bon;
            hp_bon = pd.hp_bon;

            // Calculate with bonuses
            w_speed = pd.w_speed;
            r_speed = pd.r_speed;
            atk = pd.atk + atk_bon;
            def = pd.def + def_bon;
            hp = pd.hp + hp_bon;
            energy = pd.energy;
            isWearingArmor = pd.isWearingArmor;
        }
    }

    public static void AddBonuses(int a, int h, int d) {
        atk_bon = a;
        hp_bon = h;
        def_bon = d;
        atk += atk_bon;
        hp += hp_bon;
        def += def_bon;
    }
    
}
