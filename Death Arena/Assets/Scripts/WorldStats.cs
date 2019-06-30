using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStats : MonoBehaviour
{
    public static int level;
    public static int gold;
    public static int knowledge_crystals;

    void Start() {
        DefaultStats();

        // Save new world data file if needed
        SaveSystem.SaveNewWorldData();

        LoadAll();
    }

    public void DefaultStats() {
        level = 1;
        gold = 0;
        knowledge_crystals = 0;
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
