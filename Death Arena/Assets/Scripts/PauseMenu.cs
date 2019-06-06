using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject main;
    public GameObject settings;
    public GameObject confirmation;

    public void openSettings() {
        main.SetActive(false);
        settings.SetActive(true);

        GameSettings.FindVolume();
    }

    public void closeSettings() {
        main.SetActive(true);
        settings.SetActive(false);
    }

    public void openConfirmation() {
        confirmation.SetActive(true);
    }

    public void closeConfirmation() {
        confirmation.SetActive(false);
    }

    public void resumeGame() {
        GameSettings.paused = false;
    }

    public void returnTitle() {
        SaveSystem.LoadData();
        SceneManager.LoadScene("MainMenu");
    }

    void Update() {

        if (GameSettings.paused) {
            menu.SetActive(true);
            // Stop all animations
            Animator[] anims = (Animator[]) GameObject.FindObjectsOfType(typeof(Animator));
            foreach (Animator anim in anims) {
                anim.speed = 0;
            }
        }
        else {
            menu.SetActive(false);
            closeConfirmation();
            // Resume all animations
            Animator[] anims = (Animator[]) GameObject.FindObjectsOfType(typeof(Animator));
            foreach (Animator anim in anims) {       
                anim.speed = 1;
            }
        }

        
    }
}
