using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ArmoryData
{
    // Armor
    public bool[] armorBought = new bool[1];
    // Weapons
    public bool[] weaponBought;


    public ArmoryData() {
        for (int i = 0; i < armorBought.Length; i++) {
            armorBought[i] = ArmorSet.GetIfBought(i);
        }
    }
}
