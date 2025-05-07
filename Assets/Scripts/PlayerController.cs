using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float horizontalInput;
    public float jumpForce;

    private Rigidbody2D playerRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.position = new Vector2 (transform.position.x + horizontalInput * speed * Time.deltaTime, transform.position.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }

    }
}
