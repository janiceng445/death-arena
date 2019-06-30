using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iron : Item
{
    protected override void Start() {
        // Variables
        index = 0;
        cost = 150;
        attackBuff = 0;
        healthBuff = 150;
        defBuff = 5;
        type = 1;

        // Defaults
        image = null;
        itemName = "Placeholder";
        itemRefName = "Iron";

        // Call functions
        FinalizeStart();
    }
}
