using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEconomy : MonoBehaviour
{
    public void AddMoney(int amount) {
        WorldStats.gold += amount;
    }

    public void RemoveMoney(int amount) {
        if (WorldStats.gold - amount < 0) {
            WorldStats.gold = 0;
        }
        else {
            WorldStats.gold -= amount;
        }
    }
}
