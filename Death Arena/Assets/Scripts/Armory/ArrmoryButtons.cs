using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ArrmoryButtons : MonoBehaviour
{

    GameObject armorTab;
    GameObject weaponTab;
    public Text attack;
    public Text health;
    public Text defense;
    public Text energy;
    public Text money;

    void Start() {
        armorTab = GameObject.Find("Armor Tab");
        weaponTab = GameObject.Find("Weapons Tab");
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
    }

    void Update() {
    }

    public void ReturnTitle() {
        SaveSystem.SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    public void ViewArmory() {
        armorTab.SetActive(true);
        weaponTab.SetActive(false);
    }

    public void ViewWeaponry() {
        armorTab.SetActive(false);
        weaponTab.SetActive(true);
    }
}
