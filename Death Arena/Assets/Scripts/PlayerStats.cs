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

    // Armor
    public static bool isWearingArmor;

    void Start() {
        // The default
        w_speed = 35f;
        r_speed = 45f;
        atk = 10;
        def = 5;
        hp = 100;
        energy = 100;
        isWearingArmor = false;

        // Save new player data file if needed
        SaveSystem.SaveNewPlayerData();

        // Load actual player data file
        PlayerData pd = SaveSystem.LoadPlayerData();
        if (pd != null) {
            w_speed = pd.w_speed;
            r_speed = pd.r_speed;
            atk = pd.atk;
            def = pd.def;
            hp = pd.hp;
            energy = pd.energy;
            isWearingArmor = pd.isWearingArmor;
        }
    }
    
}
