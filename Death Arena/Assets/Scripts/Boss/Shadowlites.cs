using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadowlites : MonoBehaviour
{
    //** Shadowlite Stats **// 
    Rigidbody2D rb; 
    private float moveSpeed; 
    private SpriteRenderer shadowSprite; 
    private Collider2D hitboxCollider; 
    private Collider2D bubbleCollider; 
    // Animator
    Animator shadowliteAnimator; 
    public bool isLunging; 

    //** Player **// 
    GameObject target; 
    Transform targetLocation; 
    Vector3 directionToTarget; 
    private bool facingRight; 
    
    //** Parent Paratoria **//
    private Paratoria Paratoria; 
    
    void Start ()
    {
        // Player 
        target = GameObject.Find("Player"); 
        targetLocation = target.GetComponent<Transform>(); 

        // Shadowlite
        rb = GetComponentInParent<Rigidbody2D>(); 
        hitboxCollider = gameObject.transform.GetChild(4).GetComponent<BoxCollider2D>(); 
        hitboxCollider.enabled = true; 
        bubbleCollider = gameObject.transform.GetChild(5).GetComponent<CircleCollider2D>(); 
        bubbleCollider.enabled = true;
        moveSpeed = 7f; 
        // shadowSprite = GetComponent<SpriteRenderer>(); 
        shadowliteAnimator = GetComponent<Animator>(); 

        // Paratoria 
        Paratoria = GameObject.Find("Wraith(Clone)").transform.GetChild(0).GetComponent<Paratoria>(); 
    }
    
    void Update ()
    {
        if (Paratoria.NightmareAbility)
        {
            MoveMonster (); 
            bubbleCollider.enabled = false; 
        }
        else
        {
            bubbleCollider.enabled = true; 
        }
        shadowliteAnimator.SetBool("isLunging", isLunging); 
        // Flip
        if (transform.parent.transform.position.x < targetLocation.position.x && !facingRight) {
            Flip();
        }
        else if (transform.parent.transform.position.x > targetLocation.position.x && facingRight) {
            Flip();
        }
    }

    IEnumerator shadowSpriteEnabler()
    {
        shadowSprite.enabled = false;
        hitboxCollider.enabled = false; 
        yield return new WaitForSeconds(2);
        shadowSprite.enabled = true; 
        hitboxCollider.enabled = true; 
    }

    void MoveMonster ()
    {
        if (target != null)
        {
            isLunging = true;
            directionToTarget = (target.transform.position - transform.position).normalized; 
            rb.velocity = new Vector2 (directionToTarget.x * moveSpeed, directionToTarget.y * moveSpeed); 
        }
        else
        {
            rb.velocity = Vector3.zero; 
        }
    }

    public void DestroyThis()
    {
        isLunging = false; 
        Paratoria.spawnCount--; 
        Destroy(transform.parent.gameObject);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 flipScale = transform.parent.transform.localScale;
		flipScale.x *= -1;
		transform.parent.transform.localScale = flipScale;
    }
}
