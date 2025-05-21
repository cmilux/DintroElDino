using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private GameManager gameManager;

    void Start()
    { 
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();

        collision.gameObject.GetComponent<PlayerHealth>();
        

        if (playerHealth == null)
        {

        }
        else 
        {
            playerHealth.DamagePlayer();
        }
        */

        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.DamagePlayer();
            
        }
    }
}