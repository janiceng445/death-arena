using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    }

    void Update() {
        attack.text = "Attack: " + PlayerStats.atk.ToString();
        health.text = "Health: " + PlayerStats.hp.ToString();
        defense.text = "Defense: " + PlayerStats.def.ToString();
        energy.text = "Energy: " + PlayerStats.energy.ToString();
        money.text = "Money: " + WorldStats.gold.ToString();
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
