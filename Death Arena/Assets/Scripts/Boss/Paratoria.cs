using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paratoria : Boss
{
    //** Spawn Shadowlites **//
    public GameObject enemy; 
    float randX; 
    float randY; 
    Vector2 whereToSpawn; 
    public float spawnRate;  
    float nextSpawn = 0.0f; 

    //** Abilities **//
    private bool midstAbility; 
    // Radius around that slows 
    public Collider2D SlowRadius; 

    //** Moving Mechanics **// 
    private bool canMove;
    // Timer that goes up
    private float moveTimer; 
    // Random number that Timer goes up to in order to determine when Paratoria moves
    private float randomMoveTimer; 

    protected override void Start() {
        base.Start();

        // Set stats
        health = 500;
        Speed = 1.5f;
        breathDuration = 1f;
        moneyAmount = 1000;

        ResetBreathTimer();
        CompleteStats();

        // Shadowlites
        spawnRate = 5f; 

        // Pick a number in the beginning to initiate the cycle of random timers
        randomMoveTimer = Random.Range (1f, 6f);

        // Slow zone enabler
        SlowRadius = transform.GetChild(1).GetComponent<CircleCollider2D>(); 
        SlowRadius.enabled = false; 
    }
    protected override void Update() 
    {
        // Spawning Shadowlites
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate; 
            randX = Random.Range (-23f, 23f);
            randY = Random.Range (-10f, 10f);
            whereToSpawn = new Vector2 (randX, randY); 
            //Instantiate (enemy, whereToSpawn, Quaternion.identity);
        }
    
        // Moving
        Move();
        moveTimer += 0.01f; 
        // Debug.Log(moveTimer); 
        // Debug.Log("TIMER: " + randomMoveTimer); 
    }
    protected override void Move() {
        if (!GameSettings.paused) {
            // Fix sorting order
            sprite.sortingOrder = Mathf.RoundToInt(transform.parent.transform.position.y * 100f) * -1;

            // If moveTimer (increasing 0.01f) has reached the random number timer, allowed to move
            if (moveTimer >= randomMoveTimer) {
                canMove = true; 
                if (canMove && !midstAbility) {
                    isMoving = true;
                    SlowRadius.enabled = true; 
                    if (Vector2.Distance(myLocation.position, targetLocation.position) > DistanceAway)
                    {
                        transform.parent.transform.position = Vector2.MoveTowards(transform.parent.transform.position, targetLocation.position, Speed * Time.deltaTime); 
                        if (transform.parent.transform.position.x < targetLocation.position.x && !FacingRight) {
                            base.Flip();
                        }
                        else if (transform.parent.transform.position.x > targetLocation.position.x && FacingRight) {
                            base.Flip();
                        }
                    }
                    // After 5 seconds, stop moving and reset
                    if (moveTimer >= randomMoveTimer + 5)
                    {
                        moveTimer = 0; 
                        randomMoveTimer = Random.Range (3f, 7f);
                    }
                }
            }
            // Until moveTimer has reached the random number timer, Paratoria will not be able to move, nor slow
            else{
                canMove = false;
                SlowRadius.enabled = false; 
            }
        }
    }
}
