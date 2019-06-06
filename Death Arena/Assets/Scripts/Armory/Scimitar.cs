using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scimitar : Item
{
    protected override void Start() {
        index = 0;
        cost = 200;
        itemRefName = "Scimitar";
        type = 2;

        attackBuff = 50;
        healthBuff = 0;
        defBuff = 0;

        FinalizeStart();
    }
}
