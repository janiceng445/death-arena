using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private float spawnDelay;
    private float spawnTimer;

    void Start() {
        spawnDelay = 0.2f;
        spawnTimer = spawnDelay;
    }

    void Update()
    {
        // Spawn footsteps
        if (gameObject.GetComponent<PlayerController>().isMoving) {
            GameObject footsteps = null;
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                spawnTimer = spawnDelay;
                Vector3 location = transform.position;
                location.y -= 1.25f;
                footsteps = Instantiate(Resources.Load<GameObject>("Prefabs/footsteps"), location, Quaternion.identity);
            }
            Destroy(footsteps, 1f);
        }
    }
}
