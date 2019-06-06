using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEMPORARYMENU : MonoBehaviour
{
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
    }
}
