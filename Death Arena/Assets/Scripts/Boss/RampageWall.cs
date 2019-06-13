using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampageWall : MonoBehaviour
{
    private Minotaur MinotaurRampage;

    void Start()
    {
        MinotaurRampage = GameObject.Find("Minotaur").transform.GetChild(0).GetComponent<Minotaur>(); 
    }
    
    void OnTriggerExit2D (Collider2D col)
    {
        if (col.name == "Minotaur")
        {
            MinotaurRampage.isCharging = false; 
        }
    }
}
