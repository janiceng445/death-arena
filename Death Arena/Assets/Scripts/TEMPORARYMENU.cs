using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TEMPORARYMENU : MonoBehaviour
{
    public MenuStats menustats;

    // ** TEST ONLY ** //
    public InputField TestMoneyField;
    public void TestSetMoney() {
        WorldStats.gold = int.Parse(TestMoneyField.text);
    }
    public InputField Testhealth;
    public void TestSetHealth() {
        PlayerStats.hp = int.Parse(Testhealth.text);
    }
    public void NextLevel() {
        WorldStats.level++;
    }
    public void ResetLevel() {
        WorldStats.level = 1;
        SaveSystem.SaveData();
    }
    public void DeleteFiles() {
        SaveSystem.DeleteSaveDirectory();
        GameObject.Find("GameManager").GetComponent<PlayerStats>().DefaultStats();
        GameObject.Find("GameManager").GetComponent<WorldStats>().DefaultStats();
        GameObject.Find("GameManager").GetComponent<Armory>().DefaultStats();
        SaveSystem.SaveNewArmoryData();
        SaveSystem.SaveNewPlayerData();
        SaveSystem.SaveNewSpellbookData();
        SaveSystem.SaveNewWorldData();
        SceneManager.LoadScene("MainMenu");
        SaveSystem.LoadData();
        menustats.ReloadScene();
    }
}
