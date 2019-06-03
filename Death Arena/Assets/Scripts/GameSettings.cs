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
        if (scene.name == "Arena") {
            GetComponent<AudioSource>().enabled = false;
        }
        else if (!GetComponent<AudioSource>().enabled) {
            GetComponent<AudioSource>().enabled = true;
        }
    }
 
    public static void FindVolume() {
        // Update the new volume reference
        GameObject volumePanel = GameObject.FindGameObjectWithTag("Volume");
        if (volumePanel != null) {
            volumePanel.GetComponent<Slider>().value = volume;
            UpdateAllAudio();
        }
    }

    void Start() {
        // The very start of the entire game
        paused = true;
        volume = 1f;
    }

    void Update()
    {
        // Update volume of game based on slider
        GameObject volumePanel = GameObject.FindGameObjectWithTag("Volume");
        if (volumePanel != null) {
            volume = volumePanel.GetComponent<Slider>().value;
            AudioListener.volume = volume;
        }
        UpdateAllAudio();
    }

    public static void UpdateAllAudio() {
        AudioSource[] audiosources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audio in audiosources) {
            audio.volume = volume;
        }
    }
}
