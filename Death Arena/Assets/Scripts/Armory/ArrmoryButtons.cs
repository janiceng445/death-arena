using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ArrmoryButtons : MonoBehaviour
{

    private GameObject armorTab;
    private GameObject weaponTab;
    public Text attack;
    public Text health;
    public Text defense;
    public Text energy;
    public Text money;
    private GameObject currArmorObject;
    private GameObject currWeaponObject;
    private ToggleGroup armorToggleGroup;
    private ToggleGroup weaponToggleGroup;

    void Start() {
        armorTab = GameObject.Find("Armor Tab");
        weaponTab = GameObject.Find("Weapons Tab");
        armorToggleGroup = armorTab.GetComponent<ToggleGroup>();
        weaponToggleGroup = weaponTab.GetComponent<ToggleGroup>();
        armorTab.SetActive(true);
        weaponTab.SetActive(false);
        ResetText();
    }

    public void ResetText() {
        attack.text = "Attack: " + PlayerStats.atk.ToString();
        health.text = "Health: " + PlayerStats.hp.ToString();
        defense.text = "Defense: " + PlayerStats.def.ToString();
        energy.text = "Energy: " + PlayerStats.energy.ToString();
        money.text = "Money: " + WorldStats.gold.ToString();
        attack.color = new Color32(0x32, 0x32, 0x32, 0xFF);
        health.color = new Color32(0x32, 0x32, 0x32, 0xFF);
        defense.color = new Color32(0x32, 0x32, 0x32, 0xFF);
        energy.color = new Color32(0x32, 0x32, 0x32, 0xFF);
        money.color = new Color32(0x32, 0x32, 0x32, 0xFF);
    }

    void Update() {
        if (armorToggleGroup.ActiveToggles().FirstOrDefault() != null) {
            currArmorObject = armorToggleGroup.ActiveToggles().FirstOrDefault().gameObject;
        }
        if (weaponToggleGroup.ActiveToggles().FirstOrDefault() != null) {
            currWeaponObject = weaponToggleGroup.ActiveToggles().FirstOrDefault().gameObject;
        }
    }

    public void ReturnTitle() {
        SaveSystem.SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    public void ViewArmory() {
        armorTab.SetActive(true);
        weaponTab.SetActive(false);
        if (currArmorObject != null) currArmorObject.GetComponent<Toggle>().isOn = true;
    }

    public void ViewWeaponry() {
        armorTab.SetActive(false);
        weaponTab.SetActive(true);
        if (currWeaponObject != null) currWeaponObject.GetComponent<Toggle>().isOn = true;
    }
}
