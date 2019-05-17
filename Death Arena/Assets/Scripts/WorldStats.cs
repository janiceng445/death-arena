using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStats : MonoBehaviour
{
    public static int level;
    public static int gold;

    void Start() {
        level = 1;
        gold = 1500000;
    }

    void Update() {
    }
}
