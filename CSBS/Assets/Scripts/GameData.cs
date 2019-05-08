using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    // Clock Data
    public static int hour = 0;
    public static int ticker = 0;

    // Bar Data
    [Range(0.0f, 1.0f)] public float StressLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float HappinessLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float HungerLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float TemperatureLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float GermsLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float EnergyLvl = 1.0f;
    private GameObject bars;
    Transform stress_bar;
    Transform happinness_bar;
    Transform hunger_bar;
    Transform temperature_bar;
    Transform germs_bar;
    Transform energy_bar;

    // Colors
    Color danger = new Color(255,0,0);
    Color alert = new Color(255, 213, 76);
    Color moderate = new Color(59, 229, 90);
    Color good = new Color(255,255,255);
    Color cold = new Color(0, 93, 255);
    Color room = new Color(214, 255, 218);
    Color hot = new Color(255, 63, 0);

    void Start() {
        bars = GameObject.Find("Bars");
        stress_bar = bars.transform.Find("Stress/StressBar");
        happinness_bar = bars.transform.Find("Happiness/HappinessBar");
        hunger_bar = bars.transform.Find("Hunger/HungerBar");
        temperature_bar = bars.transform.Find("Temperature/TemperatureBar");
        germs_bar = bars.transform.Find("Germs/GermsBar");
        energy_bar = bars.transform.Find("Energy/EnergyBar");
    }

    void Update() {
        // Clock
        ticker++;
        if (ticker >= 100) {
            ticker = 0;
            hour++;
        }
        if (hour >= 24) {
            hour = 0;
        }

        // Bars
        stress_bar.localScale = new Vector3(1.0f, StressLvl);
        happinness_bar.localScale = new Vector3(1.0f, HappinessLvl);
        hunger_bar.localScale = new Vector3(1.0f, HungerLvl);
        temperature_bar.localScale = new Vector3(1.0f, TemperatureLvl);
        germs_bar.localScale = new Vector3(1.0f, GermsLvl);
        energy_bar.localScale = new Vector3(1.0f, EnergyLvl);
        UpdateColorBars();
    }

    void UpdateColorBars() {
        SpriteRenderer stress = GameObject.Find("StressBar/BarSprite").GetComponent<SpriteRenderer>();
        // Stress
        Color lerpedColor = Color.Lerp(Color.white, Color.black, StressLvl);
        stress.color = lerpedColor;
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
