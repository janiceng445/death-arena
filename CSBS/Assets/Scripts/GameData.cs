using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    private static bool instance;

    // Clock Data
    public static int hour = 0;
    public static int ticker = 0;

    // Bar Data
    [Range(0.0f, 1.0f)] public float StressLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float SanityLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float HungerLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float TemperatureLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float GermsLvl = 1.0f;
    [Range(0.0f, 1.0f)] public float TirednessLvl = 1.0f;
    private GameObject bars;
    Transform stress_bar;
    Transform sanity_bar;
    Transform hunger_bar;
    Transform temperature_bar;
    Transform germs_bar;
    Transform tiredness_bar;

    // Colors
    Color danger = new Color(255/255,0/255,0/255);
    Color alert = Color.yellow;
    Color good = new Color(112/255, 255/255, 121/255);

    void Start() {
        // Bars
        bars = GameObject.Find("Bars");
        stress_bar = bars.transform.Find("Stress/StressBar");
        sanity_bar = bars.transform.Find("Sanity/SanityBar");
        hunger_bar = bars.transform.Find("Hunger/HungerBar");
        temperature_bar = bars.transform.Find("Temperature/TemperatureBar");
        germs_bar = bars.transform.Find("Germs/GermsBar");
        tiredness_bar = bars.transform.Find("Tiredness/TirednessBar");

        // Default Colors
        GameObject.Find("Bars/Stress/StressBar/BarSprite").GetComponent<SpriteRenderer>().color = good;
        GameObject.Find("Bars/Sanity/SanityBar/BarSprite").GetComponent<SpriteRenderer>().color = Color.green;
        GameObject.Find("HungerBar/BarSprite").GetComponent<SpriteRenderer>().color = good;
        GameObject.Find("TemperatureBar/BarSprite").GetComponent<SpriteRenderer>().color = good;
        GameObject.Find("GermsBar/BarSprite").GetComponent<SpriteRenderer>().color = good;
        GameObject.Find("TirednessBar/BarSprite").GetComponent<SpriteRenderer>().color = good;
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
        sanity_bar.localScale = new Vector3(1.0f, SanityLvl);
        hunger_bar.localScale = new Vector3(1.0f, HungerLvl);
        temperature_bar.localScale = new Vector3(1.0f, TemperatureLvl);
        germs_bar.localScale = new Vector3(1.0f, GermsLvl);
        tiredness_bar.localScale = new Vector3(1.0f, TirednessLvl);
        UpdateColorBars();
    }

    void UpdateColorBars() {
        SpriteRenderer stress = GameObject.Find("StressBar/BarSprite").GetComponent<SpriteRenderer>();
        SpriteRenderer sanity = GameObject.Find("SanityBar/BarSprite").GetComponent<SpriteRenderer>();
        SpriteRenderer hunger = GameObject.Find("HungerBar/BarSprite").GetComponent<SpriteRenderer>();
        SpriteRenderer temperature = GameObject.Find("TemperatureBar/BarSprite").GetComponent<SpriteRenderer>();
        SpriteRenderer germs = GameObject.Find("GermsBar/BarSprite").GetComponent<SpriteRenderer>();
        SpriteRenderer tiredness = GameObject.Find("TirednessBar/BarSprite").GetComponent<SpriteRenderer>();
        
        stress.color = StressLvl < 0.5f ? Color.Lerp(good, alert, StressLvl*2) : Color.Lerp(alert, danger, (StressLvl-0.5f) * 2);
        sanity.color = SanityLvl < 0.5f ? Color.Lerp(good, alert, SanityLvl*2) : Color.Lerp(alert, danger, (SanityLvl-0.5f) * 2);
        hunger.color = HungerLvl < 0.5f ? Color.Lerp(good, alert, HungerLvl*2) : Color.Lerp(alert, danger, (HungerLvl-0.5f) * 2);
        temperature.color = TemperatureLvl < 0.5f ? Color.Lerp(good, alert, TemperatureLvl*2) : Color.Lerp(alert, danger, (TemperatureLvl-0.5f) * 2);
        germs.color = GermsLvl < 0.5f ? Color.Lerp(good, alert, GermsLvl*2) : Color.Lerp(alert, danger, (GermsLvl-0.5f) * 2);
        tiredness.color = TirednessLvl < 0.5f ? Color.Lerp(good, alert, TirednessLvl*2) : Color.Lerp(alert, danger, (TirednessLvl-0.5f) * 2);
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        if (!instance) {
            instance = true;
        }
        else {
            DestroyObject(this.gameObject);
        }
    }
}
