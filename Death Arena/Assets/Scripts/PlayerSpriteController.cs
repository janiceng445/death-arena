using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    void Call_EnableAttackCollision() {
        this.transform.parent.GetComponent<PlayerController>().EnableAttackCollision();
    }

    void Call_FinishAttacking() {
        this.transform.parent.GetComponent<PlayerController>().FinishAttacking();
    }

    void Call_DisableRolling() {
        this.transform.parent.GetComponent<PlayerController>().DisableRolling();
    }
}
