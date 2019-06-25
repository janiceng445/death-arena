using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Wolf : Boss
{

    private SortingGroup sorting;


    protected override void Start() {
        base.Start();

        // Set stats
        power = 200;
        health = 2500;
        Speed = 5f;
        breathDuration = 1f;
        moneyAmount = 3000;

        ResetBreathTimer();
        CompleteStats();
        sorting = gameObject.GetComponent<SortingGroup>();
        DistanceAway = 1f;
    }

    protected override void Update() {
        base.Update();
        base.Move();
        // Fix sorting order
        sorting.sortingOrder = Mathf.RoundToInt(transform.parent.transform.position.y * 100f) * -1;
        if (health <= 0 && !dieOnce) {
            base.Die();
        }

        if (inRange && !isAttacking && !isTakingBreak) {
            isAttacking = true;
        }
    }
}
