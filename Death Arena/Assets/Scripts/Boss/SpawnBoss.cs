using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public static bool bossReady = false;
    private bool spawned = false;

    void Start() {
    }

    void Update() {        
        if (bossReady && !spawned) {
            Instantiate(Resources.Load<GameObject>("Prefabs/Minotaur"), new Vector3(0,0,0), Quaternion.identity);
            spawned = true;
        }
    }
}
