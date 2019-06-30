using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampageWall : MonoBehaviour
{
    private Minotaur MinotaurRampage;

    void Update()
    {
        MinotaurRampage = GameObject.Find("Minotaur(Clone)").transform.GetChild(0).GetComponent<Minotaur>(); 
    }
    
    void OnTriggerExit2D (Collider2D col)
    {
        if (col.name == "Minotaur(Clone)")
        {
            MinotaurRampage.isCharging = false; 
        }
    }
}
