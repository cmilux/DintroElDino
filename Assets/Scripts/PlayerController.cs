using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 5f;
    private float _horizontalInput;
    public float _jumpForce = 7f;
    public float xRange = 8.75f;
    public float yRange = -7f;

    private Rigidbody2D _playerRb;

    private bool playerIsOnGround = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
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
        _horizontalInput = Input.GetAxis("Horizontal");
        _playerRb.linearVelocityX = _horizontalInput * _speed;
    }

    void PlayerJump()
    {
        //Player jump
        if (Input.GetKeyDown(KeyCode.Space) & playerIsOnGround)
        {
            _playerRb.AddForceY(_jumpForce, ForceMode2D.Impulse);
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
        if (_horizontalInput > 0)
        {
            transform.localScale = new Vector2(1, 1); // Face right
        }
        else if (_horizontalInput < 0)
        {
            transform.localScale = new Vector2(-1, 1); // Face left
        }
    }

    //Prevent the player of infite jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerIsOnGround = true;
        }

        else if (collision.gameObject.CompareTag("Platform"))
        {
            playerIsOnGround = true;
        }

        else if (collision.gameObject.CompareTag("Spike"))
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
