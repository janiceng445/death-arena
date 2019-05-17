using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Data
    public static float w_speed;
    public static float r_speed;
    public static int atk;
    public static int def;
    public static int hp;
    public static int energy;

    void Start() {
        w_speed = 20f;
        r_speed = 30f;
        atk = 10;
        def = 5;
        hp = 100;
        energy = 100;
    }
    
}
