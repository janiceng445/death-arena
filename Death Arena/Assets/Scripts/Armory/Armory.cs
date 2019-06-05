using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Armory : MonoBehaviour
{
    public static bool[] armorBought;
    public static bool[] weaponBought;

    void Start() {
        armorBought = new bool[2];
        weaponBought = new bool[2];
        for (int i = 0; i < armorBought.Length; i++) {
            armorBought[i] = false;
        }
        for (int i = 0; i < weaponBought.Length; i++) {
            weaponBought[i] = false;
        }

        // Save new armory data file if needed
        SaveSystem.SaveNewArmoryData();

        // Load actual armor data file
        ArmoryData ad = SaveSystem.LoadArmoryData();
        if (ad != null) {
            armorBought = ad.armorBought;
            weaponBought = ad.weaponBought;
        }
    }
}
