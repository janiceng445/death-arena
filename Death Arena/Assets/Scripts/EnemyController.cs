using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public float DistanceAway = 3f;
    private bool FacingRight = true;
    private SpriteRenderer sprite;

    private Transform target;

    void Start() {
        Speed = Random.Range(1f, 4f);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        
    }

    void Update() {
        // Fix sorting order
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        // Move
        if (Vector2.Distance(transform.position, target.position) > DistanceAway) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
        
        // Flip
        if (transform.position.x < target.position.x && !FacingRight) {
            Flip();
        }
        else if (transform.position.x > target.position.x && FacingRight) {
            Flip();
        }
    }

    void Flip() {
		FacingRight = !FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

}
