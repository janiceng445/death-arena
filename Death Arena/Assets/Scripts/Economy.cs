using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{
    private Text InGameText;

    void Start() {
        InGameText = GameObject.FindGameObjectWithTag("GameMoneyText").GetComponent<Text>();
    }

    void Update() {
        InGameText.text = WorldStats.gold.ToString("n0");
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
