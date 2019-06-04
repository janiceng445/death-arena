using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour
{
    public static bool[] armorBought;

    void Start() {
        armorBought = new bool[2];
        for (int i = 0; i < armorBought.Length; i++) {
            armorBought[i] = false;
        }

        // Save new armory data file if needed
        SaveSystem.SaveNewArmoryData();

        // Load actual armor data file
        ArmoryData ad = SaveSystem.LoadArmoryData();
        if (ad != null) {
            armorBought = ad.armorBought;
        }
    }
}
