using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

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
