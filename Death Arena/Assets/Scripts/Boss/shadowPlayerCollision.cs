using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowPlayerCollision : MonoBehaviour
{
    private Paratoria Paratoria;
    private Shadowlites Shadowlites; 

    void Start ()
    {
        Paratoria = GameObject.Find("Wraith").transform.GetChild(0).GetComponent<Paratoria>(); 
        Shadowlites = GetComponentInParent<Shadowlites>(); 
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.name == "Player" && Paratoria.NightmareAbility)
        {
            Debug.Log("Life steal. Corrupt. Consume.");
            Shadowlites.DestroyThis(); 
        }
    }
}
