using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public bool bossReady = false;
    private bool spawned = false;
    private string namePrefab;
    public static bool bossAlive;
    private GameObject bossBar;
    
    // Return to title
    private int ReturnToTitle_timer;
    private bool IncLvlOnce = false;

    // Stats upgrade for player
    private int hp_up;
    private int atk_up;
    private int def_up;

    void Start() {
        // Error checking with world stats
        if (WorldStats.level == 0) {
            WorldStats.level = 1;
        }

        // Initialization
        bossBar = GameObject.Find("BossBar");
        bossBar.SetActive(false);
        ReturnToTitle_timer = 200;
        bossAlive = true;
        switch(WorldStats.level) {
            case 1:
                namePrefab = "Ogre";
                break;
            case 2:
                namePrefab = "Minotaur";
                break;
        }

        // Player ups
        hp_up = (WorldStats.level * 10) + (int) (0.2f * PlayerStats.hp);
        atk_up = (WorldStats.level * 2) + (int) (0.2f * PlayerStats.atk);
        def_up  = WorldStats.level * 2;
    }

    void Update() {        
        // Spawning
        if (bossReady && !spawned) {
            Instantiate(Resources.Load<GameObject>("Prefabs/" + namePrefab), new Vector3(0,0,0), Quaternion.identity);
            spawned = true;
            bossBar.SetActive(true);
        }

        // If boss is dead, get ready to return to menu
        if (!bossAlive) {
            ReturnToTitle_timer--;
            if (ReturnToTitle_timer <= 0 && !IncLvlOnce) {
                IncLvlOnce = true;
                WorldStats.level += 1;
                PlayerStats.hp += hp_up;
                PlayerStats.atk += atk_up;
                PlayerStats.def += def_up;
                SaveSystem.SaveData();
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
