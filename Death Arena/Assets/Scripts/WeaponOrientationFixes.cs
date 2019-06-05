using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrientationFixes : MonoBehaviour
{
    SpriteRenderer weapon;
    void Start()
    {
        weapon = gameObject.GetComponent<SpriteRenderer>();
        weapon.flipX = false;
        weapon.flipY = false;

        
        // Flip both X and Y
        if (PlayerStats.weaponName == "Axe1") {
            weapon.flipX = true;
            weapon.flipY = true;
        }
        else if(PlayerStats.weaponName == "Scimitar") {
            weapon.flipX = true;
        }
    }
}
