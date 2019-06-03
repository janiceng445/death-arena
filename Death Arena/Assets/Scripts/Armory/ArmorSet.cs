using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSet : MonoBehaviour
{
    protected bool isBought;
    protected int cost;
    protected Image image;
    protected string itemName;
    protected int healthBuff;
    protected string itemRefName;
    protected GameObject itemReference;

    protected virtual void Start() {
        isBought = false;
        cost = 150;
        image = null;
        itemName = "Placeholder";
        healthBuff = 15;
        itemRefName = "iron";
        itemReference = GameObject.Find(itemRefName);
        itemReference.GetComponentInChildren<Text>().text = cost.ToString();
    }

    protected virtual void Update() {
    }

    public virtual void Buy() {
        if (WorldStats.gold >= cost) {
            isBought = true;
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
            WorldStats.gold -= cost;
        }
        else {
            Debug.Log("Not enough gold");
        }
    }
}
