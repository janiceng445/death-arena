using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    protected GameObject toggleReference;
    protected GameObject toggleGroupReference;

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

    protected virtual void FinalizeStart() {
        toggleReference = GetComponentInChildren<Toggle>().gameObject;
        toggleReference.SetActive(false);
        toggleGroupReference = GameObject.FindObjectOfType<ToggleGroup>().gameObject;
    }

    protected virtual void UpdateBought() {
        // Update what has been bought from saved data
        if (Armory.armorBought != null) {
            isBought = Armory.armorBought[index];
        }
        if (isBought) {
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
            toggleReference.SetActive(true);
            if (PlayerStats.armorSetName == itemRefName) {
                toggleReference.GetComponent<Toggle>().isOn = true;
            }
        }
    }

    protected virtual void Update() {
        // Getting name of holding toggle
        if (toggleGroupReference.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault() != null) {
            GameObject currArmorObject = GameObject.FindObjectOfType<ToggleGroup>().ActiveToggles().FirstOrDefault().transform.parent.gameObject;
            string currArmorSet = currArmorObject.name;
            Sprite[] currSprites = currArmorObject.GetComponent<ArmorSet>().sprites;
            GameObject.Find("Player").GetComponent<PlayerGear>().SetGear(currSprites, currArmorSet);
        }
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
            
            // Enabling the toggle
            toggleReference.SetActive(true);
            toggleReference.GetComponent<Toggle>().isOn = true;

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
