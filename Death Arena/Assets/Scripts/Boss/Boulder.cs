using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private GameObject player; 
    private Vector3 playerLocation;
    private Collider2D boulderCol; 
    public float speed;
    private bool hitPlayer = false; 
    private bool isTraveling;
    
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        playerLocation = player.transform.position; 
        boulderCol = GetComponent<CircleCollider2D>(); 
        speed = 35.0f; 
    }

    void Update ()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt((transform.position.y - 0.5f) * 100f) * -1;
        Vector3 currPlayerLocation = playerLocation;
        float boulderSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currPlayerLocation, boulderSpeed);
        if (transform.position != currPlayerLocation) {
            isTraveling = true;
        }
        else {
            isTraveling = false;
        }
    }  

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player" && isTraveling)
        {
            GetComponent<Animator>().Play("minotaur_boulder_explode"); 
        }
    }
}