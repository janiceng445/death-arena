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
    private GameObject weapon_back;
    private GameObject weapon_hand;
    private string[] names;
    
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
        weapon_back = GameObject.Find("weapon");
        weapon_hand = GameObject.Find("WeaponHitbox");
        if (PlayerStats.armorSetName != null) {
            LoadArmorSet(PlayerStats.armorSet, PlayerStats.armorSetName); 
        }
        if (PlayerStats.weaponName != null) {
            LoadWeapon();
        }
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
        if (!PlayerStats.hasNewWeapon) {
            Sprite broadsword = Resources.Load<Sprite>("Armor/w_broadsword");
            weapon_back.GetComponent<SpriteRenderer>().sprite = broadsword;
            weapon_hand.GetComponent<SpriteRenderer>().sprite = broadsword;
        }
    }

    public void SetGear(Sprite[] sprites, string name) {
        PlayerStats.armorSetName = name;
        helmet.GetComponent<SpriteRenderer>().sprite = sprites[0];
        chest.GetComponent<SpriteRenderer>().sprite = sprites[1];
        upper_arm_left.GetComponent<SpriteRenderer>().sprite = sprites[2];
        lower_arm_left.GetComponent<SpriteRenderer>().sprite = sprites[3];
        upper_arm_right.GetComponent<SpriteRenderer>().sprite = sprites[4];
        lower_arm_right.GetComponent<SpriteRenderer>().sprite = sprites[5];
        upper_leg_left.GetComponent<SpriteRenderer>().sprite = sprites[6];
        lower_leg_left.GetComponent<SpriteRenderer>().sprite = sprites[7];
        upper_leg_right.GetComponent<SpriteRenderer>().sprite = sprites[8];
        lower_leg_right.GetComponent<SpriteRenderer>().sprite = sprites[9];
        GetNamesImages(sprites);
    }

    public void SetWeapon(Sprite[] sprites, string name) {
        PlayerStats.weaponName = name;
        weapon_back.GetComponent<SpriteRenderer>().sprite = sprites[0];
        weapon_hand.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    public void GetNamesImages(Sprite[] sprites) {
        names = new string[sprites.Length];
        for (int i = 0; i < names.Length; i++) {
            if (sprites[i] != null) 
                names[i] = sprites[i].name.ToString();
            else
                names[i] = "null";
        }
        PlayerStats.armorSet = names;
    }

    public void LoadArmorSet(string[] names, string setName) {
        // Match name of images and sprite
        Dictionary<string, Sprite> spriteSet = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Armor/" + setName);
        foreach(Sprite sprite in sprites) {
            spriteSet.Add(sprite.name, sprite);
        }

        // Assign sprites to renderer
        helmet.GetComponent<SpriteRenderer>().sprite = names[0] != "null" ? spriteSet[names[0]] : null;
        chest.GetComponent<SpriteRenderer>().sprite = names[1] != "null" ? spriteSet[names[1]] : null;
        upper_arm_left.GetComponent<SpriteRenderer>().sprite = names[2] != "null" ? spriteSet[names[2]] : null;
        lower_arm_left.GetComponent<SpriteRenderer>().sprite = names[3] != "null" ? spriteSet[names[3]] : null;
        upper_arm_right.GetComponent<SpriteRenderer>().sprite = names[4] != "null" ? spriteSet[names[4]] : null;
        lower_arm_right.GetComponent<SpriteRenderer>().sprite = names[5] != "null" ? spriteSet[names[5]] : null;
        upper_leg_left.GetComponent<SpriteRenderer>().sprite = names[6] != "null" ? spriteSet[names[6]] : null;
        lower_leg_left.GetComponent<SpriteRenderer>().sprite = names[7] != "null" ? spriteSet[names[7]] : null;
        upper_leg_right.GetComponent<SpriteRenderer>().sprite = names[8] != "null" ? spriteSet[names[8]] : null;
        lower_leg_right.GetComponent<SpriteRenderer>().sprite = names[9] != "null" ? spriteSet[names[9]] : null;
    }

    public void LoadWeapon() {
        Sprite sprite = Resources.Load<Sprite>("Armor/" + PlayerStats.weaponName);
        weapon_back.GetComponent<SpriteRenderer>().sprite = sprite;
        weapon_hand.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
