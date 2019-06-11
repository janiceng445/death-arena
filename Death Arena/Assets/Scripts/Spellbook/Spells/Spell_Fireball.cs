using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Fireball : SorcerySpells
{
    protected override void Start() {
        base.Start();
        spellname = "Fireball";
        spellpower = 15;
        duration = 2f;
        cooldown = 5f;
        description = "Shoots a fireball at a single enemy target.";
        id = 1;
        CheckCondition();
    }
}
