using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject settings;

    public void LoadGame() {
        SceneManager.LoadScene("Arena");
        WorldStats.paused = false;
    }

    public void LoadArmory() {
        SceneManager.LoadScene("Armory");
    }

    public void LoadSkills() {
        SceneManager.LoadScene("Skills");
    }

    public void LoadSettings() {
        settings.SetActive(true);
    }

    public void UnLoadSettings() {
        settings.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
