using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Boss
{
    
    public GameObject aliveSprite;
    public GameObject deathSprite;

    protected override void Start() {
        base.Start();

        // Set stats
        power = 20;
        health = 500;
        Speed = 3f;
        breathDuration = 1f;
        moneyAmount = 1000;

        weaponCollider = gameObject.transform.Find("bone_1/bone_2/bone_3/weapon").gameObject.GetComponent<BoxCollider2D>();

        ResetBreathTimer();
        CompleteStats();
    }

    protected override void Update() {
        base.Update();
        base.Move();
        if (health <= 0 && !dieOnce) {
            Die();
        }

        if (inRange && !isAttacking && !isTakingBreak) {
            isAttacking = true;
        }
    }

    protected override void Die() {
        isDead = true;
        WorldStats.gold += moneyAmount;
        dieOnce = true;
        this.enabled = false;
        BossManager.bossAlive = false;
        BossManager.bossAlive = false;
        aliveSprite.SetActive(false);
        deathSprite.SetActive(true);
    }
}
