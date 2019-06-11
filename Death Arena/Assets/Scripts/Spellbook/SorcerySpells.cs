using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    ID tags:
    fire = 1
    water = 2
    leaf = 3
    moon = 4
    sun = 5
    meteor = 6
    lightning = 7
 */

public class SorcerySpells : MonoBehaviour
{
    // References
    protected GameObject selector;
    protected GameObject LeftPage;
    protected Animator animator;
    protected Image elementImage;
    protected Text nameText;
    protected Text spellpowerText;
    protected Text durationText;
    protected Text cooldownText;
    protected Text descriptionText;

    // Sprites
    protected Dictionary<string, Sprite> elementSheet;
    protected Sprite[] sprites;

    // Stats
    protected bool unlocked;
    protected string spellname;
    protected int spellpower;
    protected float duration;
    protected float cooldown;
    protected string description;
    protected string imageName;
    protected int id;

    protected virtual void Start() {
        selector = GameObject.Find("Sorce_Selector");
        selector.GetComponent<SpriteRenderer>().enabled = false;
        unlocked = false;
    }

    protected virtual void OnMouseOver() {
        if (Input.GetMouseButton(0)) {
            FindReferences();
            if (!Sorcery.spellSelected) selector.transform.position = this.transform.position;
            else Sorcery.newPosSelector = this.transform.position;
            Sorcery.spellIDSelected = id;
            selector.GetComponent<SpriteRenderer>().enabled = true;
            UpdateContent();
        }
    }

    public void CheckCondition() {
        if (Spellbook.spellsUnlocked != null) {
            unlocked = Spellbook.spellsUnlocked[id - 1];
        }
    }

    protected virtual void UpdateContent() {
        elementImage.enabled = true;
        animator.Play("PageSorceryContent", -1, 0f);
        nameText.text = spellname;
        spellpowerText.text = "Spellpower: " + spellpower.ToString();
        durationText.text = "Duration: " + duration.ToString();
        cooldownText.text = "Cooldown: " + cooldown.ToString();
        descriptionText.text = description;
        elementImage.preserveAspect = false;
        elementImage.sprite = null;
        elementImage.sprite = elementSheet[imageName];
        elementImage.preserveAspect = true;
    }

    protected void FindReferences() {
        LeftPage = GameObject.Find("Page1").transform.GetChild(0).gameObject;
        animator = GameObject.Find("Page1").GetComponent<Animator>();
        elementImage = LeftPage.transform.GetChild(0).transform.Find("sorce_img").GetComponent<Image>();
        elementImage.enabled = false;
        imageName = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
        nameText = LeftPage.transform.GetChild(1).transform.Find("sorce_name").GetComponent<Text>();
        spellpowerText = LeftPage.transform.GetChild(1).transform.Find("sorce_spell").GetComponent<Text>();
        durationText = LeftPage.transform.GetChild(1).transform.Find("sorce_duration").GetComponent<Text>();
        cooldownText = LeftPage.transform.GetChild(1).transform.Find("sorce_cooldown").GetComponent<Text>();
        descriptionText = LeftPage.transform.Find("sorce_desc").GetComponent<Text>();
        elementSheet = new Dictionary<string, Sprite>();
        sprites = Resources.LoadAll<Sprite>("Spellbook/sorcery");
        foreach(Sprite sprite in sprites) {
            elementSheet.Add(sprite.name, sprite);
        }
    }

}
