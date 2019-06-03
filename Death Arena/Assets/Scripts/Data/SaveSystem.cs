using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    const string PlayerFileName = "/player.dat";
    const string WorldFileName = "/world.dat";
    const string ArmoryFileName = "/armory.dat";

    public static void SaveData() {
        SavePlayerData();
        SaveWorldData();
        SaveArmoryData();
    }

    public static void LoadData() {
        LoadPlayerData();
        LoadWorldData();
        LoadArmoryData();
    }


    // Save
    #region Saving Methods
    public static void SavePlayerData() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PlayerFileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveWorldData() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + WorldFileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        WorldData data = new WorldData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveArmoryData() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ArmoryFileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        ArmoryData data = new ArmoryData();

        formatter.Serialize(stream, data);
        stream.Close();
    }
    #endregion

    // Save new data
    #region Saving New Methods
    public static void SaveNewPlayerData() {
        string path = Application.persistentDataPath + PlayerFileName;
        if (!File.Exists(path)) {
            SavePlayerData();
        }
    }
    public static void SaveNewWorldData() {
        string path = Application.persistentDataPath + WorldFileName;
        if (!File.Exists(path)) {
            SaveWorldData();
        }
    }
    public static void SaveNewArmoryData() {
        string path = Application.persistentDataPath + ArmoryFileName;
        if (!File.Exists(path)) {
            SaveArmoryData();
        }
    }
    #endregion

    // Load
    #region Loading Methods
    public static PlayerData LoadPlayerData() {
        string path = Application.persistentDataPath + PlayerFileName;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("No player save file found.");
            return null;
        }
    }

    public static WorldData LoadWorldData() {
        string path = Application.persistentDataPath + WorldFileName;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            WorldData data = formatter.Deserialize(stream) as WorldData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("No world save file found.");
            return null;
        }
    }

    public static ArmoryData LoadArmoryData() {
        string path = Application.persistentDataPath + ArmoryFileName;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ArmoryData data = formatter.Deserialize(stream) as ArmoryData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("No armory save file found.");
            return null;
        }
    }
    #endregion
}
