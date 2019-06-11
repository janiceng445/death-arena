using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected int spellpower;
    protected float duration;
    protected float cooldown;

    private bool canCast;
    private bool isCasting;
    private bool isCooldown;

    // Timers
    protected float durationTimer;
    protected float cooldownTimer;

    public void setConditions(int spellpower, float duration, float cooldown) {
        this.spellpower = spellpower;
        this.duration = duration;
        this.cooldown = cooldown;
    }

    void Start() {
        canCast = true;
    }

    protected virtual void Update() {
        if (Input.GetKeyDown(KeyCode.F) && canCast) {
            isCasting = true;
            canCast = false;
        }
        Timers();
    }

    protected virtual void Timers() {
        if (isCasting) {
            durationTimer -= Time.deltaTime;
            DoAbility();            
        }
        if (durationTimer <= 0) {
            durationTimer = duration;
            isCasting = false;
            isCooldown = true;
        }
        if (isCooldown) {
            cooldownTimer -= Time.deltaTime;
        }
        if (cooldownTimer <= 0) {
            cooldownTimer = cooldown;
            isCooldown = false;
            canCast = true;
        }
    }

    protected virtual void DoAbility() {
    }
}
