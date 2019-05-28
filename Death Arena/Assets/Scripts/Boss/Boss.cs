using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    protected bool FacingRight;
    protected float Speed;
    public int health;
    protected int power;
    protected float DistanceAway = 3f;
    protected int breathDuration;
    protected int breathTimer;

    protected SpriteRenderer sprite;
    protected BoxCollider2D weaponCollider;
    protected Animator animator;
    protected GameObject target;
    protected Transform targetLocation;

    // Base Booleans
    public bool inRange;
    protected bool isMoving;
    protected bool isAttacking;
    protected bool isTakingBreak;

    protected virtual void Start() {
        // Initialize some variables
        sprite = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetLocation = target.GetComponent<Transform>();
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
            breathTimer--;
            if (breathTimer <= 0) {
                isTakingBreak = false;
                ResetBreathTimer();
            }
        }
    }

    protected virtual void Move() {
        if (!GameSettings.paused) {
            // Fix sorting order
            sprite.sortingOrder = Mathf.RoundToInt(transform.parent.transform.position.y * 100f) * -1;

            if (!isAttacking && !isTakingBreak) {
                // Move
                isMoving = true;
                if (Vector2.Distance(transform.parent.transform.position, targetLocation.position) > DistanceAway) {
                    transform.parent.transform.position = Vector2.MoveTowards(transform.parent.transform.position, targetLocation.position, Speed * Time.deltaTime);
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

    public virtual void Attack() {
        target.GetComponent<PlayerConditions>().health -= power;
    }

    public virtual void TakeDamage(int amount) {
        health -= amount;
    }

    protected virtual void Die() {
        if (health <= 0) {
            Debug.Log("boss dies");
        }
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

    void Flip() {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.parent.transform.localScale;
		theScale.x *= -1;
		transform.parent.transform.localScale = theScale;
    }

}
