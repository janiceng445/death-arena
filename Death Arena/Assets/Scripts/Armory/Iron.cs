using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iron : ArmorSet
{
    protected override void Start() {
        // Variables
        index = 0;
        cost = 150;
        attackBuff = 0;
        healthBuff = 50;
        defBuff = 5;

        // Defaults
        isBought = false;
        image = null;
        itemName = "Placeholder";
        itemRefName = "Iron";
        itemReference = GameObject.Find(itemRefName);
        itemReference.GetComponentInChildren<Text>().text = cost.ToString();

        // Call functions
        FinalizeStart();
        UpdateBought();
    }
}
