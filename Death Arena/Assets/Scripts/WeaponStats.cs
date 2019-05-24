using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public static int weapon_power;

    void Start() {
        // Default
        weapon_power = 10;
    }

    public void SetPower(int amount) {
        weapon_power = amount;
    }

}
