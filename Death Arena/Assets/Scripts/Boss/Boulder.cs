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
    
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        playerLocation = player.transform.position; 
        boulderCol = GetComponent<CircleCollider2D>(); 
        speed = 35.0f; 
    }

    void Update ()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        Vector3 currPlayerLocation = playerLocation;
        float boulderSpeed = speed * Time.deltaTime;
        if (!hitPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, currPlayerLocation, boulderSpeed);
        }
    }  

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            hitPlayer = true; 
        }
    }

    void Destroy ()
    {
        Destroy(gameObject); 
    }
}