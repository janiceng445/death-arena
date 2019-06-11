using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    protected override void DoAbility() {
        GameObject fireball = Instantiate(Resources.Load<GameObject>("Prefabs/fireball"), transform.position, Quaternion.identity);
        Destroy(fireball, 1f);
    }
}
