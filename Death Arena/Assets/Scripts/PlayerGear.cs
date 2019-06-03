using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGear : MonoBehaviour
{
    private GameObject helmet;
    private GameObject hair;
    private GameObject shoulder_left;
    private GameObject shoulder_right;
    private GameObject breastplate;
    
    void Start() {
        helmet = GameObject.Find("helmet");
        hair = GameObject.Find("hair");
        shoulder_left = GameObject.Find("shoulderpad_l");
        shoulder_right = GameObject.Find("shoulderpad_r");
        breastplate = GameObject.Find("breastplate");
    }

    void Update() {
        if (PlayerStats.isWearingArmor) {
            helmet.GetComponent<SpriteRenderer>().enabled = true;
            hair.GetComponent<SpriteRenderer>().enabled = false;
            shoulder_left.GetComponent<SpriteRenderer>().enabled = true;
            shoulder_right.GetComponent<SpriteRenderer>().enabled = true;
            breastplate.GetComponent<SpriteRenderer>().enabled = true;
        }
        else {
            helmet.GetComponent<SpriteRenderer>().enabled = false;
            hair.GetComponent<SpriteRenderer>().enabled = true;
            shoulder_left.GetComponent<SpriteRenderer>().enabled = false;
            shoulder_right.GetComponent<SpriteRenderer>().enabled = false;
            breastplate.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
