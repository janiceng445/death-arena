using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public bool bossReady = false;
    private bool spawned = false;
    private string namePrefab;
    public static bool bossAlive;
    
    // Return to title
    private int ReturnToTitle_timer;
    private bool IncLvlOnce = false;

    void Start() {
        // Error checking with world stats
        if (WorldStats.level == 0) {
            WorldStats.level = 1;
        }

        // Initialization
        ReturnToTitle_timer = 500;
        bossAlive = true;
        switch(WorldStats.level) {
            case 1:
                namePrefab = "Ogre";
                break;
            case 2:
                namePrefab = "Minotaur";
                break;
        }
    }

    void Update() {        
        // Spawning
        if (bossReady && !spawned) {
            Instantiate(Resources.Load<GameObject>("Prefabs/" + namePrefab), new Vector3(0,0,0), Quaternion.identity);
            spawned = true;
        }

        // If boss is dead, get ready to return to menu
        if (!bossAlive) {
            ReturnToTitle_timer--;
            if (ReturnToTitle_timer <= 0 && !IncLvlOnce) {
                IncLvlOnce = true;
                WorldStats.level += 1;
                SaveSystem.SaveData();
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
