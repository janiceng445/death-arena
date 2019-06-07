using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*
    When adding a new item to shop, you must do the following:
    1. Create a script with name that matches .png file (case sensitive) plus all updated contents of script
    2. Drag and drop appropriate sprites in the Sprites[] found in the Inspector of the script
    3. Update the image of the item
    4. Configure the set script (on value change)
    5. Configure the button script (on click)
    6. Update the array sizes found in Armory for respective type
    7. Add the name of the script to "WeaponOrientationFixes" script if flipping is needed
 */

public class Item : MonoBehaviour
{
    protected int index;
    public bool isBought;
    public int cost;
    protected int type;
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
    }

    protected virtual void FinalizeStart() {
        isBought = false;
        itemReference = GameObject.Find(itemRefName);
        itemReference.GetComponentInChildren<Text>().text = cost.ToString();
        toggleReference = GetComponentInChildren<Toggle>().gameObject;
        toggleReference.SetActive(false);
        toggleGroupReference = GameObject.FindObjectOfType<ToggleGroup>().gameObject;
        UpdateBought();
    }

    protected virtual void UpdateBought() {
        // Update what has been bought from saved data
        if (type == 1) {
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
        else if (type == 2) {
            if (Armory.weaponBought != null) {
                isBought = Armory.weaponBought[index];
            }
            if (isBought) {
                itemReference.GetComponentInChildren<Button>().interactable = false;
                itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
                toggleReference.SetActive(true);
                if (PlayerStats.weaponName == itemRefName) {
                    toggleReference.GetComponent<Toggle>().isOn = true;
                }
            }
        }
    }

    protected virtual void Update() {

        // Getting name of holding toggle
        if (type == 1) {
            if (toggleGroupReference.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault() != null) {
                GameObject currArmorObject = GameObject.FindObjectOfType<ToggleGroup>().ActiveToggles().FirstOrDefault().transform.parent.gameObject;
                string currArmorSet = currArmorObject.name;
                Sprite[] currSprites = currArmorObject.GetComponent<Item>().sprites;
                GameObject.Find("Player").GetComponent<PlayerGear>().SetGear(currSprites, currArmorSet);
                PlayerStats.AddBonuses(currArmorObject.GetComponent<Item>().attackBuff, currArmorObject.GetComponent<Item>().healthBuff, currArmorObject.GetComponent<Item>().defBuff);
            }
        }
        else if (type == 2) {
            if (toggleGroupReference.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault() != null) {
                GameObject currWeaponObject = GameObject.FindObjectOfType<ToggleGroup>().ActiveToggles().FirstOrDefault().transform.parent.gameObject;
                string currWeaponName = currWeaponObject.name;
                Sprite[] currSprites = currWeaponObject.GetComponent<Item>().sprites;
                GameObject.Find("Player").GetComponent<PlayerGear>().SetWeapon(currSprites, currWeaponName);
                PlayerStats.AddBonusesWeapon(currWeaponObject.GetComponent<Item>().attackBuff, currWeaponObject.GetComponent<Item>().healthBuff, currWeaponObject.GetComponent<Item>().defBuff);
            }
        }
    }

    public virtual void Buy() {
        if (WorldStats.gold >= cost) {
            // Make the basic changes and audio
            isBought = true;
            AudioClip purchaseSFX = (AudioClip) Resources.Load("SFX/purchase", typeof(AudioClip));
            GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(purchaseSFX);
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
            WorldStats.gold -= cost;
            
            // Enabling the toggle
            toggleReference.SetActive(true);
            toggleReference.GetComponent<Toggle>().isOn = true;

            // Set what was bought in Armory via index
            if (type == 1) {
                PlayerStats.isWearingArmor = true;
                Armory.armorBought[index] = isBought;
                SetBuffsArmor();
                AssignSpritesArmor();
            }
            else if (type == 2) {
                PlayerStats.hasNewWeapon = true;
                Armory.weaponBought[index] = isBought;
                SetBuffsWeapon();
                AssignSpritesWeapon();
            }

            // Reset texts
            GameObject.Find("Canvas").GetComponent<ArrmoryButtons>().ResetText();

            // Save
            SaveSystem.SaveArmoryData();
            SaveSystem.SaveWorldData();
        }
        else {
            Debug.Log("Not enough gold");
        }
    }

    protected void AssignSpritesArmor() {
        GameObject.Find("Player").GetComponent<PlayerGear>().SetGear(sprites, itemRefName);
    }

    protected void AssignSpritesWeapon() {
        GameObject.Find("Player").GetComponent<PlayerGear>().SetWeapon(sprites, itemRefName);
    }

    protected void SetBuffsArmor() {
        PlayerStats.AddBonuses(attackBuff, healthBuff, defBuff);
    }

    protected void SetBuffsWeapon() {
        PlayerStats.AddBonusesWeapon(attackBuff, healthBuff, defBuff);
    }

    public void UpdateToggle() {
        GameObject.Find("Canvas").GetComponent<ArrmoryButtons>().ResetText();
    }
}
