using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paratoria : MonoBehaviour
{
    public GameObject enemy; 
    float randX; 
    float randY; 
    Vector2 whereToSpawn; 
    public float spawnRate;  
    float nextSpawn = 0.0f; 

    void Start ()
    {
        spawnRate = 5f; 
    }
    void Update ()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate; 
            randX = Random.Range (-23f, 23f);
            randY = Random.Range (-10f, 10f);
            whereToSpawn = new Vector2 (randX, randY); 
            Instantiate (enemy, whereToSpawn, Quaternion.identity); 
        }
    }
}
