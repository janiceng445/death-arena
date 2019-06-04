using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Steel : ArmorSet
{
    protected override void Start() {
        // Variables
        index = 1;
        cost = 500;
        attackBuff = 0;
        healthBuff = 100;
        defBuff = 10;

        // Defaults
        isBought = false;
        image = null;
        itemName = "Placeholder";
        itemRefName = "Steel";
        itemReference = GameObject.Find(itemRefName);
        itemReference.GetComponentInChildren<Text>().text = cost.ToString();

        // Call functions
        UpdateBought();
    }
}
