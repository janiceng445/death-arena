using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStats : MonoBehaviour
{
    public static int level;
    public static int gold;
    public static int knowledge_crystals;

    void Start() {
        level = 1;
        gold = 5000;
        knowledge_crystals = 5000;

        // Save new world data file if needed
        SaveSystem.SaveNewWorldData();

        LoadAll();
    }

    void LoadAll() {
        // Load world data
        WorldData wd = SaveSystem.LoadWorldData();
        if (wd != null) {
            level = wd.level;
            gold = wd.gold;
            knowledge_crystals = wd.knowledge_crystals;
        }
    }
}
