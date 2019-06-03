using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iron : ArmorSet
{
    protected override void Start() {
        index = 0;
        isBought = false;
        cost = 150;
        image = null;
        itemName = "Placeholder";
        healthBuff = 15;
        itemRefName = "iron";
        itemReference = GameObject.Find(itemRefName);
        itemReference.GetComponentInChildren<Text>().text = cost.ToString();
    }
}
