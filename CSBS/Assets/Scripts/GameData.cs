using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    private static bool instance;

    // Status
    public static bool isFainted = false;
    public static bool isTired = true;
    private bool init = false;

    // Clock Data
    public static int hour = 0;
    public static int ticker = 0;

    // Bar Data
    [Range(0.0f, 1.0f)] public static float StressLvl = 0;
    [Range(0.0f, 1.0f)] public static float SanityLvl = 0;
    [Range(0.0f, 1.0f)] public static float HungerLvl = 0;
    [Range(0.0f, 1.0f)] public static float TemperatureLvl = 0;
    [Range(0.0f, 1.0f)] public static float GermsLvl = 0;
    [Range(0.0f, 1.0f)] public static float TirednessLvl = 0;
    private GameObject bars;
    Transform stress_bar, sanity_bar, hunger_bar, temperature_bar, germs_bar, tiredness_bar;
    bool stress_maxed, sanity_maxed, hunger_maxed, temperature_maxed, germs_maxed, tiredness_maxed = false;
    float DangerLevel = 0.75f;

    // Status
    public static bool fed;

    // Colors
    Color danger = new Color(255/255,0/255,0/255);
    Color alert = Color.yellow;
    Color good = new Color(112/255, 255/255, 121/255);

    // References
    GameObject food;

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

        // Stress and Sanity
        if (StressLvl > DangerLevel || SanityLvl > DangerLevel) {
            GameObject.Find("Alert").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Alert").GetComponent<Animator>().enabled = true;
        }
        else {
            GameObject.Find("Alert").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Alert").GetComponent<Animator>().enabled = false;
        }

        // Tiredness
        SpriteRenderer faint = GameObject.Find("Fainting").GetComponent<SpriteRenderer>();
        Color temp = faint.color;
        if (TirednessLvl > 0.5f && TirednessLvl != 1f) {
            temp.a = (TirednessLvl - 0.5f);
        }
        else if (TirednessLvl == 1f) {
            temp.a += 0.001f;
            if (temp.a >= 1f) isFainted = true;
        }
        else {
            temp.a = 0;
        }
        faint.color = temp;

        // Others
        UpdateColorBars();
        AffectHealth();
        CheckHealth();
        CheckMaxMeter();
    }

    void AffectHealth() {
        // Hunger
        if (!hunger_maxed && !fed) {
            HungerLvl += 0.0005f;
        }
        else if (hunger_maxed) {
            SanityLvl += 0.001f;
        }

        // Tiredness
        if (!tiredness_maxed && isTired) {
            TirednessLvl += 0.0001f;
        }
    }
    void CheckMaxMeter() {
        if (StressLvl > 1.0f) {
            StressLvl = 1.0f;
            stress_maxed = true;
        } else if (StressLvl < 0) StressLvl = 0;
        else stress_maxed = false;

        if (SanityLvl > 1.0f) {
            SanityLvl = 1.0f;
            sanity_maxed = true;
        } else if (SanityLvl < 0) SanityLvl = 0;
        else sanity_maxed = false;

        if (HungerLvl > 1.0f) {
            HungerLvl = 1.0f;
            hunger_maxed = true;
        } else if (HungerLvl < 0) HungerLvl = 0;
        else hunger_maxed = false;

        if (TemperatureLvl > 1.0f) {
            TemperatureLvl = 1.0f;
            temperature_maxed = true;
        } else if (TemperatureLvl < 0) TemperatureLvl = 0;
        else temperature_maxed = false;

        if (GermsLvl > 1.0f) {
            GermsLvl = 1.0f;
            germs_maxed = true;
        } else if (GermsLvl < 0) GermsLvl = 0;
        else germs_maxed = false;

        if (TirednessLvl > 1.0f) {
            TirednessLvl = 1.0f;
            tiredness_maxed = true;
        } else if (TirednessLvl < 0) TirednessLvl = 0;
        else tiredness_maxed = false;
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

    void CheckHealth() {
        if (isFainted) {
            Debug.Log("Game over");
        }
    }

    // Scene Management
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    } 

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Assigning camera
        GameObject.Find("Canvas").GetComponent<Canvas>().worldCamera = Camera.main;
        
        // Disable food GUI
        if (SceneManager.GetActiveScene().name != "MainScene") {
            GameObject.Find("Food").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("FoodGround").GetComponent<Image>().enabled = false;
            GameObject.Find("FoodCircle").GetComponent<Image>().enabled = false;
        }
        else {
            GameObject.Find("Food").GetComponent<BoxCollider2D>().enabled = true;
            GameObject.Find("FoodGround").GetComponent<Image>().enabled = true;
            GameObject.Find("FoodCircle").GetComponent<Image>().enabled = true;
            GameObject.Find("FoodCircle").GetComponent<Image>().fillAmount = 0;
        }
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        if (!instance) {
            instance = true;
        }
        else {
            Destroy(this.gameObject);
        }
    }
}
