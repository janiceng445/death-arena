using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSet : MonoBehaviour
{
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
    }

    protected virtual void UpdateBought() {
        // Update what has been bought from saved data
        isBought = Armory.armorBought[index];
        if (isBought) {
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
        }
    }

    protected virtual void Update() {
    }

    public virtual void Buy() {
        if (WorldStats.gold >= cost) {
            PlayerStats.isWearingArmor = true;
            isBought = true;
            Armory.armorBought[index] = isBought;
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
            WorldStats.gold -= cost;
        }
        else {
            Debug.Log("Not enough gold");
        }
    }
}
