using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class WorldData
{
    public int level;
    public int gold;
    public int knowledge_crystals;

    public WorldData() {
        level = WorldStats.level;
        gold = WorldStats.gold;
        knowledge_crystals = WorldStats.knowledge_crystals;
    }
}
