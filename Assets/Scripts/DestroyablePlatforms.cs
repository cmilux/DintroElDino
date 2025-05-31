using UnityEngine;
using System.Collections;

public class DestroyablePlatforms : MonoBehaviour
{
    /*
     * THERE IS A PROBLEM
     * 
     * IF PLAYER HITS FROM THE BOTTOM IT DESTROYS IT ANYWAYS
     * IT SHOULDNT HAPPEN
     * 
    */

    public float disableDelay = 2f;
    public float reenableDelay = 5f;

    private Collider2D platformCollider;
    private SpriteRenderer platformSpriteRenderer;
    private Rigidbody2D platformRb;

    void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
        platformRb.bodyType = RigidbodyType2D.Kinematic;
        platformCollider = GetComponent<Collider2D>();
        platformSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (collision.transform.position.y > transform.position.y))
        {
            Invoke("DisablePlatform", disableDelay);
        }
    }

    void DisablePlatform()
    {
        platformCollider.enabled = false;     //collision disabled
        platformSpriteRenderer.enabled = false;       //sprite disabled
        Invoke("ReenablePlatform", reenableDelay);      //resets the blinking
    }

    void ReenablePlatform()
    {
        platformCollider.enabled = true;      //enables the collider again
        platformSpriteRenderer.enabled = true;        //enables the sprite again
    }
    
}
