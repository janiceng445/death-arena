using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    const string playerFileName = "/player.dat";
    const string WorldFileName = "/world.dat";
    const string ArmoryFileData = "/armory.dat";

    public static void SaveData() {
        SavePlayerData();
        SaveWorldData();
        SaveArmorData();
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
        string path = Application.persistentDataPath + playerFileName;
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

    public static void SaveArmorData() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ArmoryFileData;
        FileStream stream = new FileStream(path, FileMode.Create);

        ArmoryData data = new ArmoryData();

        formatter.Serialize(stream, data);
        stream.Close();
    }
    #endregion

    // Load
    #region Loading Methods
    public static PlayerData LoadPlayerData() {
        string path = Application.persistentDataPath + playerFileName;
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
        string path = Application.persistentDataPath + ArmoryFileData;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ArmoryData data = formatter.Deserialize(stream) as ArmoryData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("No armor save file found.");
            return null;
        }
    }
    #endregion
}
