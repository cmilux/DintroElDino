using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float horizontalInput;
    public float jumpForce = 7f;
    public float xRange = 8.75f;
    public float yRange = -7f;

    private bool playerIsOnGround = true;

    private Rigidbody2D _playerRb;
    
    private SpriteRenderer _spriteRenderer;

    public Animator animator;

    private AudioSource playerSounds;
    public AudioClip walkSound;
    public AudioClip jumpSound;
    //public AudioSource hurtSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerSounds = GetComponent<AudioSource>();
        //walkSound = GetComponent<AudioSource>();
        //hurtSound = GetComponent<AudioSource>();
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
        animator.SetFloat("Movement", horizontalInput);
        if (horizontalInput > 0 || horizontalInput < 0)
        {
            playerSounds.PlayOneShot(walkSound, 0.3f);
        }
        
    }

    void PlayerJump()
    {
        //Player jump
        if ((Input.GetKeyDown(KeyCode.Space) || 
            (Input.GetKeyDown(KeyCode.UpArrow) || 
            (Input.GetKeyDown(KeyCode.W)) 
            & playerIsOnGround)))
        {
            _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse);
            playerIsOnGround = false;
            animator.SetBool("IsJumping", !playerIsOnGround);
            playerSounds.PlayOneShot(jumpSound, 1f);
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
        }
        else if (horizontalInput < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    //Prevent the player of infite jumping w effector 
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
                    animator.SetBool("IsJumping", false);
                    return;
                }
            }
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            playerIsOnGround = true;
            animator.SetBool("IsAlive", false);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            playerIsOnGround = true;
            animator.SetBool("IsAlive", true);

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
