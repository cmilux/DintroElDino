using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;              //Initial player health

    public void DamagePlayer()
    {
        //Reduces player's health by one
        health--;
    }

    /*
     * Now calls from SpikeDamage        HERE JUST IN CASE
     * 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If player collides with an obstacle call DamagePlayer()
        if (collision.gameObject.CompareTag("Spike"))
        {
            DamagePlayer();
        }
        
    }
    */
}
