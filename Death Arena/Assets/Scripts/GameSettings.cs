using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public static float volume;
    public static bool paused;
    public static AudioSource MusicSource;
    public static bool isMusicOn = true;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void onDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Arena") {
            AudioClip music = (AudioClip) Resources.Load("SFX/Music/Clash Defiant", typeof(AudioClip));
            GetComponent<AudioSource>().clip = music;
            if (isMusicOn) GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
        else if (GetComponent<AudioSource>().clip.name != "Crusade - Heavy Industry" && scene.name != "Arena") {
            AudioClip music = (AudioClip) Resources.Load("SFX/Music/Crusade - Heavy Industry", typeof(AudioClip));
            GetComponent<AudioSource>().clip = music;
            if (isMusicOn) GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
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
            isMusicOn = GameObject.Find("ToggleMusic").GetComponent<Toggle>().isOn;
            MuteMusic();
        }
        UpdateAllAudio();
    }

    public static void UpdateAllAudio() {
        AudioSource[] audiosources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audio in audiosources) {
            audio.volume = volume;
        }
    }

    public void MuteMusic() {
        if (!isMusicOn) {
            MusicSource.Stop();
        }
        else {
            MusicSource.Play();
        }
    }
}
