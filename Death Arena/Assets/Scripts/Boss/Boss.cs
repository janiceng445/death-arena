using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    protected bool FacingRight;
    protected float Speed;
    public int health;
    public int maxHealth;
    protected int moneyAmount;
    protected int power;
    protected float DistanceAway = 1f;
    protected float breathDuration;
    protected float breathTimer;

    protected SpriteRenderer sprite;
    public BoxCollider2D weaponCollider;
    protected Animator animator;
    protected GameObject target;
    protected Transform targetLocation;
    public Transform myLocation;
    public Image bar;

    // Base Booleans
    public bool inRange;
    protected bool isMoving;
    protected bool isAttacking;
    protected bool isTakingBreak;
    protected bool isDead;

    // Checks    
    protected bool dieOnce = false;

    protected virtual void Start() {
        // Initialize some variables
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetLocation = target.GetComponent<Transform>();
        bar = GameObject.Find("BossHealthBar").GetComponent<Image>();
        if (myLocation == null) {
            myLocation = transform.parent.transform;
        }
    }

    protected virtual void CompleteStats() {
        maxHealth = health;
    }

    protected virtual void ResetBreathTimer() {
        breathTimer = breathDuration;
    }

    protected virtual void Update() {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isTakingBreak", isTakingBreak);

        // Cooldown between attacks
        if (isTakingBreak) {
            isAttacking = false;
            breathTimer -= Time.deltaTime;
            if (breathTimer <= 0) {
                isTakingBreak = false;
                ResetBreathTimer();
            }
        }
    }

    protected virtual void Move() {
        if (!GameSettings.paused) {
            if (target.GetComponent<PlayerConditions>().health >= 0.001f) {
                // Fix sorting order
                if (transform.name == "OgreAlive" || transform.name == "OgreDead") sprite.sortingOrder = Mathf.RoundToInt(transform.parent.transform.position.y * 100f) * -1;

                if (!isAttacking && !isTakingBreak && !inRange) {
                    // Move
                    isMoving = true;
                    if (Vector2.Distance(myLocation.position, targetLocation.position) > DistanceAway) {
                        transform.parent.transform.position = Vector2.MoveTowards(transform.parent.transform.position, targetLocation.position, Speed * Time.deltaTime);
                    }
                    else {
                        isMoving = false;
                    }
                    
                    // Flip
                    if (transform.parent.transform.position.x < targetLocation.position.x && !FacingRight) {
                        Flip();
                    }
                    else if (transform.parent.transform.position.x > targetLocation.position.x && FacingRight) {
                        Flip();
                    }
                }
                else {
                    isMoving = false;
                }
            }
        }
    }

    public virtual void Attack() {
        float calc_power = power - ((float) PlayerStats.def / 2f);
        if (target.GetComponent<PlayerConditions>().health - calc_power <= 0) {
            target.GetComponent<PlayerConditions>().health = 0;
        }
        else {
            target.GetComponent<PlayerConditions>().health -= (int) Mathf.Ceil(calc_power);
        }
    }

    public virtual void TakeDamage(int amount) {
        if (health - amount <= 0) {
            health = 0;
        }
        else {
            health -= amount;
        }
        bar.fillAmount = (float) health / (float) maxHealth;
    }

    protected virtual void Die() {
        isDead = true;
        WorldStats.gold += moneyAmount;
        dieOnce = true;
        animator.Play("death");
        this.enabled = false;
        BossManager.bossAlive = false;
    }

    protected virtual void ActivateWeaponCollision() {
        weaponCollider.enabled = true;
    }

    protected virtual void DeactivateWeaponCollision() {
        weaponCollider.enabled = false;
        isTakingBreak = true;
    }

    protected virtual void DestroyThis() {
        Destroy(gameObject.transform.parent.gameObject);    
    }

    protected void Flip() {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.parent.transform.localScale;
		theScale.x *= -1;
		transform.parent.transform.localScale = theScale;
    }

}
