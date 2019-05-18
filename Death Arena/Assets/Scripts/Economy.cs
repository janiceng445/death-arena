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
}
