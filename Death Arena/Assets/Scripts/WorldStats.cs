using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStats : MonoBehaviour
{
    public static int level;
    public static int gold;

    void Start() {
        level = 1;
        gold = 0;

        LoadAll();
    }

    void LoadAll() {
        // Load world data
        WorldData wd = SaveSystem.LoadWorldData();
        if (wd != null) {
            level = wd.level;
            gold = wd.gold;
        }
    }
}
