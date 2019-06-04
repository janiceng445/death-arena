using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGear : MonoBehaviour
{
    private GameObject helmet;
    private GameObject hair;
    private GameObject chest;
    private GameObject upper_arm_left;
    private GameObject lower_arm_left;
    private GameObject upper_arm_right;
    private GameObject lower_arm_right;
    private GameObject upper_leg_left;
    private GameObject lower_leg_left;
    private GameObject upper_leg_right;
    private GameObject lower_leg_right;
    
    void Start() {
        helmet = GameObject.Find("helmet");
        hair = GameObject.Find("hair");
        chest = GameObject.Find("breastplate");
        upper_arm_left = GameObject.Find("u_armor_arm_l");
        lower_arm_left = GameObject.Find("l_armor_arm_l");
        upper_arm_right = GameObject.Find("u_armor_arm_r");
        lower_arm_right = GameObject.Find("l_armor_arm_r");
        upper_leg_left = GameObject.Find("u_armor_leg_l");
        lower_leg_left = GameObject.Find("l_armor_leg_l");
        upper_leg_right = GameObject.Find("u_armor_leg_r");
        lower_leg_right = GameObject.Find("l_armor_leg_r");

    }

    void Update() {
        if (PlayerStats.isWearingArmor) {
            hair.GetComponent<SpriteRenderer>().enabled = false;
            helmet.GetComponent<SpriteRenderer>().enabled = true;
            chest.GetComponent<SpriteRenderer>().enabled = true;
            upper_arm_left.GetComponent<SpriteRenderer>().enabled = true;
            lower_arm_left.GetComponent<SpriteRenderer>().enabled = true;
            upper_arm_right.GetComponent<SpriteRenderer>().enabled = true;
            lower_arm_right.GetComponent<SpriteRenderer>().enabled = true;
            upper_leg_left.GetComponent<SpriteRenderer>().enabled = true;
            lower_leg_left.GetComponent<SpriteRenderer>().enabled = true;
            upper_leg_right.GetComponent<SpriteRenderer>().enabled = true;
            lower_leg_right.GetComponent<SpriteRenderer>().enabled = true;
        }
        else {
            hair.GetComponent<SpriteRenderer>().enabled = true;
            helmet.GetComponent<SpriteRenderer>().enabled = false;
            chest.GetComponent<SpriteRenderer>().enabled = false;
            upper_arm_left.GetComponent<SpriteRenderer>().enabled = false;
            lower_arm_left.GetComponent<SpriteRenderer>().enabled = false;
            upper_arm_right.GetComponent<SpriteRenderer>().enabled = false;
            lower_arm_right.GetComponent<SpriteRenderer>().enabled = false;
            upper_leg_left.GetComponent<SpriteRenderer>().enabled = false;
            lower_leg_left.GetComponent<SpriteRenderer>().enabled = false;
            upper_leg_right.GetComponent<SpriteRenderer>().enabled = false;
            lower_leg_right.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void SetImage() {
        Sprite head_img = Resources.Load<Sprite>("Armor");
    }
}
