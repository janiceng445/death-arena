using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe1 : Item
{
    protected override void Start() {
        index = 1;
        cost = 450;
        itemRefName = "Axe1";
        type = 2;

        attackBuff = 75;
        healthBuff = 0;
        defBuff = 0;

        FinalizeStart();
    }
}
