using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    void OnTriggerEnter2D() {
        //this.transform.parent.GetComponent<EnemyController>().BeginAttackAnimation();

        // Testing only
        this.transform.parent.GetComponent<EnemyController>().DealDamage(this.transform.parent.GetComponent<EnemyConditions>().atkPower);
    }
}
