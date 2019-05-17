using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStats : MonoBehaviour
{
    public static int level;
    public static int gold;
    public static bool paused;
    public static float volume;

    void Start() {
        level = 1;
        gold = 1500000;
        paused = true;
        AudioListener.volume = 0.5f;
    }

    void Update() {
        //volume = GameObject.FindGameObjectWithTag("Volume").GetComponent<Slider>().value;
    }
}
