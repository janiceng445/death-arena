using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paratoria : Boss
{
    //** Spawn Shadowlites **//
    // List 
    public GameObject enemy; 
    private GameObject enemyClone; 
    public List<GameObject> shadowliteList = new List<GameObject>(); 
    // Spawnpoints
    float randX; 
    float randY; 
    Vector2 whereToSpawn; 
    // Spawn timings
    public float spawnRate;  
    float nextSpawn = 0.0f; 
    public int spawnCount;
    private bool spawnDisable; 

    // If player has struck shadowlite
    public bool shadowAttack; 

    // Player slow
    private PlayerManager PlayerManager; 

    //** Abilities **//
    private bool midstAbility; 
    private float abilityTimer; 
    private float randomAbilityTimer; 

    //** Nightmare **//
    // Timings
    public bool NightmareAbility; 
    private float nightmareTimerPrep; 
    private bool nightmarePrepDisable;
    // Four Spawnpoints 
    private int nightmareFour; 
    private int nightmareAb1; 
    private int nightmareAb2; 
    private int nightmareAb3; 
    private int nightmareAb4;
    // Timings + Resets 
    private bool nextNightmareWave; 
    private int waveCounter; 
    // Nightmare Trap initiation 
    private Vector2 ParatoriaLocation; 
    private Vector2 playerLastLocation; 
    private GameObject LightZone; 

    // Radius around that slows 
    public Collider2D SlowRadius; 

    //** Moving Mechanics **// 
    public bool canMove;
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
        spawnRate = 4f; 
        nextSpawn = 3f; 

        // PlayerManager
        PlayerManager = target.GetComponent<PlayerManager>(); 

        // Pick a number in the beginning to initiate the cycle of random timers
        randomMoveTimer = Random.Range (1f, 6f);

        // Nightmare
        randomAbilityTimer = Random.Range (30f, 35f);
        abilityTimer = randomAbilityTimer; 

        // Slow zone enabler
        SlowRadius = transform.GetChild(1).GetComponent<CircleCollider2D>(); 
        SlowRadius.enabled = false; 
    }
    protected override void Update() 
    {
        // Spawning Shadowlites
        // If more than 7 on map, slow
        if (spawnCount >= 7)
        {
            spawnDisable = true;
            PlayerManager.isDeathSlowed = true; 
        }
        else
        {
            spawnDisable = false; 
            PlayerManager.isDeathSlowed = false; 
        }
        // Spawning next shadowlite up to 7
        if (Time.time > nextSpawn && !NightmareAbility)
        {
            nextSpawn = Time.time + spawnRate; 
            randX = Random.Range (-23f, 23f);
            randY = Random.Range (-10f, 10f);
            whereToSpawn = new Vector2 (randX, randY); 
            // If not disabled (less than 7), spawn and add to list. 
            if (!spawnDisable)
            {
                enemyClone = (GameObject) Instantiate (enemy, whereToSpawn, Quaternion.identity);
                shadowliteList.Add(enemyClone); 
                spawnCount ++; 
            }
        }

        // Nightmare Ability
        // abilityTimer is equaled to a random timer. Once abilityTimer reaches 0, start Nightmare ability 
        abilityTimer -= Time.deltaTime; 
        if (abilityTimer <= 0 && !NightmareAbility)
        {
            // Take players last location, and store it for the entirety of the ability 
            playerLastLocation = target.transform.position;
            // Trap Player
            Instantiate (Resources.Load<GameObject>("Prefabs/LightZone"), playerLastLocation, Quaternion.identity); 
            LightZone = GameObject.Find("LightZone(Clone)"); 
            NightmareAbility = true; 
        }
        // Once the player has completed 8 waves, reset 
        if (waveCounter >= 8)
        {
            NightmareAbility = false; 
            Destroy(LightZone);
            randomAbilityTimer = Random.Range (25f, 30f); 
            abilityTimer = randomAbilityTimer; 
            nightmareAb1 = 0;
            nightmareAb2 = 0;
            nightmareAb3 = 0;
            nightmareAb4 = 0;
            transform.parent.transform.position = new Vector3 (0,0,0); 
            nightmarePrepDisable = false;
            NightmareReset(); 

            waveCounter = 0;
        }
        if (NightmareAbility)
        {
            // Change background
            GameObject.Find("Darkness").GetComponent<Darkness>().ActivateDarknessBG();

            // 5 second warning / preparation so Paratoria doesn't spawn right away 
            if (!nightmarePrepDisable)
            {
                NightmareReset(); 
                nightmareTimerPrep += Time.deltaTime; 
                // Paratoria disappears off to side (dramatic effect) 
                transform.parent.transform.position = new Vector3 (-500,-500,0);
            }
            if (nightmareTimerPrep >= 5)
            {
                nextNightmareWave = true; 
                nightmarePrepDisable = true; 
                nightmareTimerPrep = 0; 
            }
            if (nextNightmareWave)
            {
                // Set Paratoria's (4) locations to be based off of the stored player location values 
                ParatoriaLocation = playerLastLocation; 

                // Keep choosing random number 1-4 if any are to be repeated more than twice. 
                do
                {
                    nightmareFour = Random.Range (1,5);
                    if (nightmareAb1 >= 1)
                    {
                        if (nightmareFour == 2 || nightmareFour == 3 || nightmareFour == 4)
                        {
                            nightmareAb1 = 0;
                        }
                    }
                    else if (nightmareAb2 >= 1)
                    {
                        if (nightmareFour == 1 || nightmareFour == 3 || nightmareFour == 4)
                        {
                            nightmareAb2 = 0;
                        }
                    }
                    else if (nightmareAb3 >= 1)
                    {
                        if (nightmareFour == 1 || nightmareFour == 2 || nightmareFour == 4)
                        {
                            nightmareAb3 = 0;
                        }
                    }
                    else if (nightmareAb4 >= 1)
                    {
                        if (nightmareFour == 1 || nightmareFour == 2 || nightmareFour == 3)
                        {
                            nightmareAb4 = 0;
                        }
                    }
                } while (nightmareAb1 == 2 || nightmareAb2 == 2 || nightmareAb3 == 2 || nightmareAb4 == 2);
                // Top left
                if (nightmareFour == 1)
                {
                    ParatoriaLocation.x -= 8;
                    ParatoriaLocation.y += 8; 
                    nightmareAb1++; 
                }
                // Top right
                else if (nightmareFour == 2)
                {
                    ParatoriaLocation.x += 8;
                    ParatoriaLocation.y += 8;
                    nightmareAb2++; 
                }
                // Bottom left
                else if (nightmareFour == 3)
                {
                    ParatoriaLocation.x -= 8; 
                    ParatoriaLocation.y -= 8; 
                    nightmareAb3++; 
                }
                // Bottom right
                else if (nightmareFour == 4)
                {
                    ParatoriaLocation.x += 8; 
                    ParatoriaLocation.y -= 8; 
                    nightmareAb4++; 
                }
                // Cast Nightmare and Flip 
                Nightmare();
                ParatoriaFlip();
                // Immediately cancel so does not spawn infinitely 
                nextNightmareWave = false; 
            }
        }

        // Moving
        Move();
        moveTimer += Time.deltaTime; 

    }

    protected override void Move() {
        if (!GameSettings.paused) {
            // Fix sorting order
            sprite.sortingOrder = Mathf.RoundToInt(transform.parent.transform.position.y * 100f) * -1;

            // If moveTimer (increasing 0.01f) has reached the random number timer, allowed to move
            if (moveTimer >= randomMoveTimer) {
                canMove = true; 
                if (canMove && !midstAbility && !NightmareAbility) {
                    isMoving = true;
                    SlowRadius.enabled = true; 
                    if (Vector2.Distance(myLocation.position, targetLocation.position) > DistanceAway)
                    {
                        transform.parent.transform.position = Vector2.MoveTowards(transform.parent.transform.position, targetLocation.position, Speed * Time.deltaTime); 
                        ParatoriaFlip();
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
    
    IEnumerator Summon()
    {
        // So long as 8 waves have not been completed yet, keep spawning and add to list 
        if (NightmareAbility && waveCounter < 8)
        {
            enemyClone = (GameObject) Instantiate(enemy, transform.position, Quaternion.identity); 
            shadowliteList.Add(enemyClone); 
        }
        yield return new WaitForSeconds(4);
        // After 4 seconds, (player must have killed shadowlite by now, thus resulting in waveCounter++, or wave complete) 
        waveCounter++; 
        Debug.Log(waveCounter); 
        // So long as 8 waves have not been completed yet, reset and choose again 1-4
        if (NightmareAbility && waveCounter < 8)
        {
            nextNightmareWave = true;
        } 
    }
    void Nightmare()
    {
        // In this ability, Paraoria cannot move, damage, 
        canMove = false;
        SlowRadius.enabled = false; 
        // Finally set Paratoria's actual location to ParatoriaLocation (which was manipulated according to 4 choices)
        transform.parent.transform.position = ParatoriaLocation; 
        StartCoroutine(Summon());
    }

    void NightmareReset()
    {
        // Destroy all shadowlites before and afer Nightmare has been casted 
        foreach (GameObject enemyClone in shadowliteList)
        {
            Destroy(enemyClone.gameObject); 
        }
        for (int i = 0; i < shadowliteList.Count; i++)
        {
            shadowliteList.RemoveAt(i); 
        }
        spawnCount = 0; 
    }

    void ParatoriaFlip()
    {
        if (transform.parent.transform.position.x < targetLocation.position.x && !FacingRight) {
            base.Flip();
        }
        else if (transform.parent.transform.position.x > targetLocation.position.x && FacingRight) {
            base.Flip();
        }
    }

    public void DealDamage()
    {
        // If Paratoria is not moving, nor casting Nightmare, nor has a shadowlite been attacked
        if (!canMove && !NightmareAbility && !shadowAttack)
        {
            TakeDamage(50); 
        }
        // Moving or not moving, take damage if Player has attacked shadowlite
        else if (shadowAttack)
        {
            TakeDamage(30);
        }
    }
    public void Heal(int regen)
    {
        // During Nightmare, if any shadowlite should touch player, heal Paratoria 
        if (health + regen >= 500)
        {
            health = 500; 
        }
        else
        {
            health += regen; 
        }
        bar.fillAmount = (float) health / (float) maxHealth;
    }
}
