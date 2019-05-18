using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class LevelData
{
    public int level;
    public int numWaves;
    public int numPerWave;

    public LevelData() {
        
    }

    public LevelData(int level, int numWaves, int numPerWave) {
        this.level = level;
        this.numWaves = numWaves;
        this.numPerWave = numPerWave;
    }
}
