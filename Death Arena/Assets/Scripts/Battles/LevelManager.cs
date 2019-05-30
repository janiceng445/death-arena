using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManager : MonoBehaviour
{
    const string fileExtension = ".leb";
    public static LevelData currLevelData;
    int currLevel;

    void Start() {
        // Dummy data
        //currLevelData = new LevelData();
    }

    public void Load() {
        // Assign
        currLevel = WorldStats.level;
        //currLevelData = LoadLevel();
    }

    public LevelData LoadLevel() {
        string path = Application.persistentDataPath + "/level" + currLevel + fileExtension;
        Debug.Log(path);
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();
            return data;
        }
        else {
            Debug.LogError("No data file found for level " + currLevel);
            return null;
        }
    }

    public void NextLevel() {
        WorldStats.level++;
        Load();
    }

    public void test() {
        Debug.Log("Level: " + currLevelData.level);
        Debug.Log("Number of waves: " + currLevelData.numWaves);
        Debug.Log("Number of mobs per wave: " + currLevelData.numPerWave);
    }
}
