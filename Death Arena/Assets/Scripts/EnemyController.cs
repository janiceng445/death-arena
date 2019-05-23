using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public float DistanceAway = 3f;
    private bool FacingRight = true;
    private SpriteRenderer sprite;
    private GameObject target;
    private Transform targetLocation;

    public bool isAttacking = false;

    void Start() {
        Speed = Random.Range(1f, 4f);
        target = GameObject.FindGameObjectWithTag("Player");
        targetLocation = target.GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        
    }

    void Update() {
        if (!GameSettings.paused) {
            // Fix sorting order
            sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

            if (!isAttacking) {
                // Move
                if (Vector2.Distance(transform.position, targetLocation.position) > DistanceAway) {
                    transform.position = Vector2.MoveTowards(transform.position, targetLocation.position, Speed * Time.deltaTime);
                }
                
                // Flip
                if (transform.position.x < targetLocation.position.x && !FacingRight) {
                    Flip();
                }
                else if (transform.position.x > targetLocation.position.x && FacingRight) {
                    Flip();
                }
            }
        }
    }

    void Flip() {
		FacingRight = !FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    public void DealDamage(int amount) {
        target.GetComponent<PlayerConditions>().health -= amount;
    }

    public void TakeDamage(int amount) {
        Debug.Log("Receive dmg");
        gameObject.GetComponent<EnemyConditions>().health -= amount;
    }

    public void BeginAttackAnimation() {
        isAttacking = true;
    }

    public void EndAttackAnimation() {
        isAttacking = false;
    }

    public void Die() {
        // TODO: Add money to bank
        Destroy(gameObject);
    }

}
