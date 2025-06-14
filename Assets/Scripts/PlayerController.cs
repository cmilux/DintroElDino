using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;                //Player's speed
    public float horizontalInput;           //Player moves left or right
    public float jumpForce = 12f;            //Jump force

    public bool playerIsOnGround = true;    //Checks if player is colliding with ground or platforms

    private Rigidbody2D _playerRb;          //Player rigid body

    private SpriteRenderer _spriteRenderer; //Player sprite renderer

    public Animator animator;               //Player animations

    //Player sound effects
    private AudioSource _playerSFX;
    public AudioClip jumpSFX;

    void Start()
    {
        //Get player's components
        _playerRb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerSFX = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Call this methods every frame
        PlayerMovement();
        PlayerJump();
        PlayerRotation();
    }

    public void PlayerMovement()
    {
        //Player horizontal movement and walking animation
        horizontalInput = Input.GetAxis("Horizontal");
        _playerRb.linearVelocityX = horizontalInput * speed;
        animator.SetFloat("Movement", horizontalInput);
    }

    public void PlayerJump()
    {
        //Player jump if the conditions are true
        if (IsJumpKeyPressed() && playerIsOnGround)
        {
            _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse);        //Jump force
            playerIsOnGround = false;                                   //Player is not colliding with ground or platforms
            animator.SetBool("IsJumping", !playerIsOnGround);      //Plays jump animation
            _playerSFX.PlayOneShot(jumpSFX, 0.5f);            //Plays jump SFX
        }
    }

    public bool IsJumpKeyPressed()
    {
        //Detect what key was pressed for jumping and return the value
        return Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.Space);
    }

    public void PlayerRotation()
    {
        //Flips the sprite depending on movement direction
        if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if player is colliding with ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerIsOnGround = true;
            animator.SetBool("IsJumping", false);       //Stops jump animation
        }

        //Prevent the player of infite jumping with platform effector 
        foreach (ContactPoint2D contact in collision.contacts)
        {
            //playerIsOnGround if contact came from below the platform
            if (contact.normal.y > 0.1f)
            {
                //Checks if player collided with a platform
                if (collision.gameObject.CompareTag("Platform"))
                {
                    playerIsOnGround = true;
                    animator.SetBool("IsJumping", false);   //Stops jump animation
                    return;
                }
            }
        }
    }
}
