﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject settings;

    public void LoadGame() {
        SaveSystem.SaveData();
        SceneManager.LoadScene("Arena");
        GameSettings.paused = false;
    }

    public void LoadArmory() {
        SceneManager.LoadScene("Armory");
    }

    public void LoadSpellbook() {
        SceneManager.LoadScene("Spellbook");
    }

    public void LoadSettings() {
        settings.SetActive(true);
        GameSettings.FindVolume();
    }

    public void UnLoadSettings() {
        settings.SetActive(false);
    }

    public void QuitGame() {
        //Application.Quit();
        if (!Application.isEditor)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
