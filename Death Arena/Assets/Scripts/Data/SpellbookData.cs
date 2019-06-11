using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SpellbookData
{
    // Sorcery
    public int sorceryID;
    public bool[] spellsUnlocked;

    public SpellbookData() {
        sorceryID = Spellbook.sorcerySetID;
        spellsUnlocked = Spellbook.spellsUnlocked;
    }
}
