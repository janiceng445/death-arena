using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ArmoryData
{
    // Armor
    public bool[] armorBought;
    // Weapons
    public bool[] weaponBought;


    public ArmoryData() {
        armorBought = Armory.armorBought;
        weaponBought = Armory.weaponBought;
    }
}
