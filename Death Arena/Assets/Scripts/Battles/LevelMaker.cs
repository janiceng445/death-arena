using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelMaker : MonoBehaviour
{
    const string fileExtension = ".leb";
    public InputField levelField;
    public InputField numWavesField;
    public InputField numPerWaveField;

    public void CreateLevel() {
        // Starting variables
        int level = 0;
        int numWaves = 0;
        int numPerWave = 0;

        // Get level data from user
        level = int.Parse(levelField.text);
        numWaves = int.Parse(numWavesField.text);
        numPerWave = int.Parse(numPerWaveField.text);

        string name = "level" + level;

        // Create the level
        LevelData newLevel = new LevelData(level, numWaves, numPerWave);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + name + fileExtension;
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, newLevel);
        stream.Close();
    }

}
