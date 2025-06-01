using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float horizontalInput;
    public float jumpForce = 7f;
    public float xRange = 8.75f;
    public float yRange = -7f;

    private Rigidbody2D _playerRb;
    private SpriteRenderer _spriteRenderer;

    private bool playerIsOnGround = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
        PlayerScreenLimit();
        PlayerRotation();

    }

    void PlayerMovement()
    {
        //Player movement
        horizontalInput = Input.GetAxis("Horizontal");
        _playerRb.linearVelocityX = horizontalInput * speed;
    }

    void PlayerJump()
    {
        //Player jump
        if (Input.GetKeyDown(KeyCode.Space) & playerIsOnGround)
        {
            _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse);
            playerIsOnGround = false;
        }
    }

    void PlayerScreenLimit()
    {
        //Keeps the player between certain screen range
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
        }
    }

    void PlayerRotation()
    {
        // Rotates sprite when moving left/right
        if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = false;
            //transform.localScale = new Vector2(1, 1); // Face right
        }
        else if (horizontalInput < 0)
        {
            _spriteRenderer.flipX = true;
            //transform.localScale = new Vector2(-1, 1); // Face left
        }
    }

    //Prevent the player of infite jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Platform"))
                {
                    playerIsOnGround = true;
                    return;
                }
            }
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            playerIsOnGround = true;
        }
    }

    /*
     * 
     * DELETED 
     * 
     * 
    //Collects the collectible
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
        }
    }

    /*
     * Dibuja la esfera sobre el jugador, se usaba para hacer las colisiones con el piso pero use OnCollisionEnter()
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere();
    }
    */
}
