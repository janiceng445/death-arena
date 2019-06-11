using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSlow : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            Minotaur.isSlowing = true; 
        }
    }
    void OnTriggerStay2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            Minotaur.isSlowing = true; 
        }
    }
    void OnTriggerExit2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            Minotaur.isSlowing = false; 
        }
    }
}
