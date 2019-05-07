using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public Text text;
    public int CurrHour;
    
    void Update()
    {
        CurrHour = GameData.hour;
        text.text = "Day " + CurrHour;
    }
}
