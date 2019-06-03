﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSet : MonoBehaviour
{
    public static bool[] armorBought;
    protected int index;
    protected bool isBought;
    protected int cost;
    protected Image image;
    protected string itemName;
    protected int healthBuff;
    protected string itemRefName;
    protected GameObject itemReference;

    protected virtual void Start() {
        index = 0;
        isBought = false;
        cost = 150;
        image = null;
        itemName = "Placeholder";
        healthBuff = 15;
        itemRefName = "iron";
        itemReference = GameObject.Find(itemRefName);
        itemReference.GetComponentInChildren<Text>().text = cost.ToString();

        // Load actual armor data file
        ArmoryData ad = SaveSystem.LoadArmoryData();
        if (ad != null) {
            armorBought = ad.armorBought;
        }
    }

    protected virtual void Update() {
        // if (armorBought[index]) {
        //     itemReference.GetComponentInChildren<Button>().interactable = false;
        //     itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
        // }
    }

    public virtual void Buy() {
        if (WorldStats.gold >= cost) {
            PlayerStats.isWearingArmor = true;
            isBought = true;
            //armorBought[index] = isBought;
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
            WorldStats.gold -= cost;
        }
        else {
            Debug.Log("Not enough gold");
        }
    }

    public static bool GetIfBought(int findIndex) {
        return armorBought[findIndex];
    }
}
