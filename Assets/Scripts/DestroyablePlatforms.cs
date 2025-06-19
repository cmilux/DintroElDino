using UnityEngine;
using System.Collections;

public class DestroyablePlatforms : MonoBehaviour
{
    public float disableDelay = 2f;         //Waiting time before disabling platforms
    public float reenableDelay = 5f;        //Waiting time before re enabling platforms

    private Collider2D _platformCollider;                //Platform's collider
    private SpriteRenderer _platformSpriteRenderer;      //Platform's sprite renderer
    private Rigidbody2D _platformRb;                     //Platform's rigid body
    void Start()
    {
        //Get the components and set rigid body to kinematic to avoid physics interactions
        _platformRb = GetComponent<Rigidbody2D>();
        _platformRb.bodyType = RigidbodyType2D.Kinematic;
        _platformCollider = GetComponent<Collider2D>();
        _platformSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the player land on top of platforms
        if (collision.gameObject.CompareTag("Player") && (collision.transform.position.y > transform.position.y))
        {
            //Start the delay to disable the platforms
            Invoke(nameof(DisablePlatform), disableDelay);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //Check if player is colliding with ground
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            playerController.playerIsOnGround = false;
        }
    }

    void DisablePlatform()
    {
        _platformCollider.enabled = false;                      //Collision disabled and player falls through
        _platformSpriteRenderer.enabled = false;                //Sprite disabled (invisible)

        Invoke(nameof(ReenablePlatform), reenableDelay);        //Reenables the platform
    }

    void ReenablePlatform()
    {
        _platformCollider.enabled = true;               //Re enables the collider
        _platformSpriteRenderer.enabled = true;         //Re enables the sprite again
    }
}
