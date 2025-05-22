using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    //Player life
    public void DamagePlayer()
    {
        health--;
       
        Debug.Log(health);

    }

    //If player collides with an obstacle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            DamagePlayer();

        }

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
    }
}
