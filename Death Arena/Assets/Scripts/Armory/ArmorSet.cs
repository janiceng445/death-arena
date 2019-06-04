using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSet : MonoBehaviour
{
    protected int index;
    public bool isBought;
    public int cost;
    protected Image image;
    protected string setName;
    protected string itemName;
    protected string itemRefName;
    protected GameObject itemReference;

    // Buff stats
    public int attackBuff;
    public int healthBuff;
    public int defBuff;

    // Sprites
    public Sprite[] sprites = new Sprite[10];

    protected virtual void Start() {
        index = 0;
        isBought = false;
        cost = 150;
        image = null;
        setName = "Placeholder";
        itemName = "Placeholder";
        healthBuff = 15;
        itemRefName = "Placeholder";
        itemReference = GameObject.Find(itemRefName);
        itemReference.GetComponentInChildren<Text>().text = cost.ToString();
    }

    protected virtual void UpdateBought() {
        // Update what has been bought from saved data
        if (Armory.armorBought != null) {
            isBought = Armory.armorBought[index];
        }
        if (isBought) {
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
        }
    }

    protected virtual void Update() {
    }

    public virtual void Buy() {
        if (WorldStats.gold >= cost) {
            AudioClip purchaseSFX = (AudioClip) Resources.Load("SFX/purchase", typeof(AudioClip));
            GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(purchaseSFX);
            PlayerStats.isWearingArmor = true;
            isBought = true;
            Armory.armorBought[index] = isBought;
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
            WorldStats.gold -= cost;
            SetBuffs();
            AssignSprites();

            // Reset texts
            GameObject.Find("Canvas").GetComponent<ArrmoryButtons>().ResetText();
        }
        else {
            Debug.Log("Not enough gold");
        }
    }

    protected void AssignSprites() {
        GameObject.Find("Player").GetComponent<PlayerGear>().SetGear(sprites, itemRefName);
    }

    protected void SetBuffs() {
        PlayerStats.AddBonuses(attackBuff, healthBuff, defBuff);
    }
}
