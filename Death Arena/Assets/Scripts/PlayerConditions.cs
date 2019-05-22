using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    public int health;

    void Start() {
        health = PlayerStats.hp;
    }
}
