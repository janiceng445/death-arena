using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Watergun : SorcerySpells
{
    protected override void Start() {
        base.Start();
        spellname = "Water Gun";
        spellpower = 20;
        duration = 2f;
        cooldown = 6f;
        description = "Shoots out a series of water bullets towards a single enemy target.";
        id = 2;
        CheckCondition();
    }
}
