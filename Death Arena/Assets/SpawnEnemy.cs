using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject myPrefab;
    int timer = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer--;
        if (timer == 0) {
            Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            timer = 100;
        }
    }
}
