﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    public bool isSlowed = false; 
    public bool isDeathSlowed = false; 

    [Range(0, .3f)] [SerializeField] private float smoothing = .05f;
    private Vector3 ref_velocity;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    public bool FacingRight;
    public int frames = 300;

    void Start() {
        body = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        AddAbility();
    }
    
    public void Move(float x, float y) {

        // Fix sorting order
        gameObject.GetComponent<SortingGroup>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        // Speed reduction
        float speedReduc;

        if (isSlowed) {
            speedReduc = 0.4f;
        }
        else if (isDeathSlowed) {
            Debug.Log("hello");
            speedReduc = 0.25f; 
        }
        else {
            speedReduc = 1f;
        }
        // Assign velocity
        Vector3 newVelocity = new Vector2(x * 10f * speedReduc, y * 10f * speedReduc);

        // Smooth out velocity and apply to character
        body.velocity = Vector3.SmoothDamp(body.velocity, newVelocity, ref ref_velocity, smoothing);

        // Fix face direction
        if (x < 0 && FacingRight) {
            Flip();
        }
        else if (x > 0 && !FacingRight) {
            Flip();
        }
    }

    void AddAbility() {
        Ability ability = gameObject.AddComponent<Fireball>();
        ability.setConditions(15, 0.001f, 2f);
    }

    void Flip() {
        // Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
}
