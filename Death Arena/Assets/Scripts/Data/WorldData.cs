using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class WorldData
{
    public int level;
    public int gold;

    public WorldData() {
        level = WorldStats.level;
        gold = WorldStats.gold;
    }
}
