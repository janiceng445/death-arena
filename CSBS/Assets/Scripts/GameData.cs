using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    // Clock Data
    public static int hour = 0;
    public static int ticker = 0;
    public int test;

    // Other Data


    void Update() {
        ticker++;
        if (ticker >= 100) {
            ticker = 0;
            hour++;
        }
        if (hour >= 24) {
            hour = 0;
        }
        test = hour;
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
