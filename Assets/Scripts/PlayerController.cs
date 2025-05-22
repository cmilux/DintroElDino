using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 5f;
    private float _horizontalInput;
    public float _jumpForce = 7f;
    public float xRange = 9.5f;

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
        //Player movement
        _horizontalInput = Input.GetAxis("Horizontal");
        _playerRb.linearVelocityX = _horizontalInput * _speed;

        //Player jump
        if (Input.GetKeyDown(KeyCode.Space) & playerIsOnGround)
        {
            _playerRb.AddForceY(_jumpForce, ForceMode2D.Impulse);
            playerIsOnGround = false;
        }

        //Keeps the player between certain screen range
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
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

    }

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
