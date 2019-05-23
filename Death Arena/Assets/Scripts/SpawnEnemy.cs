using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public Transform west;
    public Transform north;
    public Transform east;
    public Transform south;
    int timer = 100;

    // Level local information tracker
    int numEnemiesWave;
    int numEnemiesSpawned;
    int numEnemiesKilled;
    bool stopSpawn;

    void Start() {
        //numEnemiesWave = LevelManager.currLevelData.numPerWave;
        
        // TEMP
        numEnemiesWave = 5;
    }

    void Update()
    {
        if (!GameSettings.paused) {
            if (!stopSpawn) {
                timer--;
                string msg = "";
                Transform chosenDoor = null;
                if (timer == 0) {
                    // Choose an entrance
                    int door = Random.Range(0,4);
                    switch (door) {
                        case 0:
                            msg = "Enemy spawned at the West side";
                            chosenDoor = west;
                            break;
                        case 1:
                            msg = "Enemy spawned at the North side";
                            chosenDoor = north;
                            break;
                        case 2:
                            msg = "Enemy spawned at the East side";
                            chosenDoor = east;
                            break;
                        case 3:
                            msg = "Enemy spawned at the South side";
                            chosenDoor = south;
                            break;
                    }
                    
                    // Dusplay message
                    Debug.Log(msg);

                    // Spawn enemy
                    Instantiate(enemy, chosenDoor.position, Quaternion.identity);
                    numEnemiesSpawned++;
                    if (numEnemiesSpawned == numEnemiesWave) {
                        Debug.Log("Wave complete");
                        stopSpawn = true;
                    }

                    // Reset
                    timer = 100;
                }
            }
        }        
    }
}
