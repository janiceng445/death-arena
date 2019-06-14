using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerDmg : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            Minotaur.isDmging = true; 
        }
    }
    void OnTriggerStay2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            Minotaur.isDmging = false; 
        }
    }
    void OnTriggerExit2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            Minotaur.isDmging = false; 
        }
    }
}
