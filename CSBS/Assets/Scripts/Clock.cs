using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public Text text;
    public int hour = 0;
    public int ticker = 0;

    // Update is called once per frame
    void Update()
    {
        ticker++;
        if (ticker >= 100) {
            ticker = 0;
            hour++;
            text.text = "Day " + hour;
        }
        if (hour >= 24) {
            hour = 0;
        }
    }
}
