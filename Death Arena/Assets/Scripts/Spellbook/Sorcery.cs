using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sorcery : MonoBehaviour
{
    public GameObject selector;
    public static bool spellSelected;
    public static Vector3 newPosSelector;
    public static int spellIDSelected;
    public GameObject button;

    public void SetSpell() {
        if (button.GetComponentInChildren<Text>().text == "Set spell") {
            spellSelected = true;
            selector.GetComponent<SpriteRenderer>().color = new Color32(0xFF, 0x00, 0xDD, 0xFF);
            if (newPosSelector != Vector3.zero) selector.transform.position = newPosSelector;
            Spellbook.sorcerySetID = spellIDSelected;
            SaveSystem.SaveSpellbookData();
        }
    }

    public void UnlockSpell() {
        if (button.GetComponentInChildren<Text>().text == "Unlock spell") {
            Spellbook.spellsUnlocked[spellIDSelected - 1] = true;
            SaveSystem.SaveSpellbookData();
        }
    }

    void Update() {
        if (spellIDSelected != 0 && Spellbook.spellsUnlocked[spellIDSelected - 1]) {
            button.GetComponentInChildren<Text>().text = "Set spell";


            if (Spellbook.spellsUnlocked[spellIDSelected - 1] && spellIDSelected == Spellbook.sorcerySetID) {
                button.GetComponent<Button>().interactable = false;
            }
            else { 
                button.GetComponent<Button>().interactable = true;
            }
        }
        else if (spellIDSelected != 0 && !Spellbook.spellsUnlocked[spellIDSelected - 1]) {
            button.GetComponentInChildren<Text>().text = "Unlock spell";
            button.GetComponent<Button>().interactable = true;
        }
    }

}
