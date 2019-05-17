using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public GameObject screen;

    void Update() {
        if (WorldStats.paused)
            screen.SetActive(true);
        else
            screen.SetActive(false);
    }
}
