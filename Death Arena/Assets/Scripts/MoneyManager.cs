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


    // ** TEST ONLY ** //
    public InputField TestMoneyField;
    public void TestSetMoney() {
        WorldStats.gold = int.Parse(TestMoneyField.text);
    }
    public InputField Testhealth;
    public void TestSetHealth() {
        PlayerStats.hp = int.Parse(Testhealth.text);
    }
}
