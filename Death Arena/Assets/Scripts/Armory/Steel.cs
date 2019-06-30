using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Steel : Item
{
    protected override void Start() {
        // Variables
        index = 1;
        cost = 500;
        attackBuff = 0;
        healthBuff = 275;
        defBuff = 10;
        type = 1;

        // Defaults
        image = null;
        itemName = "Placeholder";
        itemRefName = "Steel";

        // Call functions
        FinalizeStart();
    }
}
