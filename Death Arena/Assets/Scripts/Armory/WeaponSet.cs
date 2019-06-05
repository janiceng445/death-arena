using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WeaponSet : MonoBehaviour
{
    protected int index;
    public bool isBought;
    public int cost;
    protected string itemRefName;
    protected GameObject itemReference;
    protected GameObject toggleReference;
    protected GameObject toggleGroupReference;

    // Buff stats
    public int attackBuff;
    public int healthBuff;
    public int defBuff;

    protected virtual void Start() {
        index = 0;
        cost = 0;
        itemRefName = "null";
        attackBuff = 0;
        healthBuff = 0;
        defBuff = 0;
        FinalizeStart();
    }

    protected virtual void FinalizeStart() {
        isBought = false;
        itemReference = GameObject.Find(itemRefName);
        itemReference.GetComponentInChildren<Text>().text = cost.ToString();
        toggleReference = GetComponentInChildren<Toggle>().gameObject;
        toggleReference.SetActive(false);
        toggleGroupReference = GameObject.FindObjectOfType<ToggleGroup>().gameObject;
    }

    protected virtual void UpdateBought() {
        // Update what has been bought from saved data
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

    protected virtual void Update() {
        // Getting name of holding toggle
        if (toggleGroupReference.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault() != null) {
            GameObject currWeaponObject = GameObject.FindObjectOfType<ToggleGroup>().ActiveToggles().FirstOrDefault().transform.parent.gameObject;
            string currWeaponName = currWeaponObject.name;
            //Sprite[] currSprites = currWeaponObject.GetComponent<ArmorSet>().sprites;
            //GameObject.Find("Player").GetComponent<PlayerGear>().SetGear(currSprites, currArmorSet);
            //PlayerStats.AddBonuses(currWeaponObject.GetComponent<ArmorSet>().attackBuff, currArmorObject.GetComponent<ArmorSet>().healthBuff, currArmorObject.GetComponent<ArmorSet>().defBuff);
            //GameObject.Find("Canvas").GetComponent<ArrmoryButtons>().ResetText();
        }
    }

        public virtual void Buy() {
        if (WorldStats.gold >= cost) {
            AudioClip purchaseSFX = (AudioClip) Resources.Load("SFX/purchase", typeof(AudioClip));
            GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(purchaseSFX);
            isBought = true;
            Armory.weaponBought[index] = isBought;
            itemReference.GetComponentInChildren<Button>().interactable = false;
            itemReference.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Bought";
            WorldStats.gold -= cost;
            
            // Enabling the toggle
            toggleReference.SetActive(true);
            toggleReference.GetComponent<Toggle>().isOn = true;

            // Reset texts
            GameObject.Find("Canvas").GetComponent<ArrmoryButtons>().ResetText();
        }
        else {
            Debug.Log("Not enough gold");
        }
    }
}
