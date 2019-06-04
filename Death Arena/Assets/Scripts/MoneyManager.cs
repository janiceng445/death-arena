using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public void SetMoney(int amount) {
        WorldStats.gold = amount;
    }

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
