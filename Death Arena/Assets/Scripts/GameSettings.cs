using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public static float volume;
    public static bool paused;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void onDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    }

    public static void FindVolume() {
        // Update the new volume reference
        GameObject volumePanel = GameObject.FindGameObjectWithTag("Volume");
        if (volumePanel != null) {
            volumePanel.GetComponent<Slider>().value = volume;
        }
    }

    void Start() {
        paused = true;
    }

    void Update()
    {
        // Update volume of game based on slider
        GameObject volumePanel = GameObject.FindGameObjectWithTag("Volume");
        if (volumePanel != null) {
            volume = volumePanel.GetComponent<Slider>().value;
            AudioListener.volume = volume;
        }
    }
}
