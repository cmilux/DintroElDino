using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int starCollectible;         //Tracks player score
    private readonly int _starValue = 10;          //Value of the collectible

    //Score SFX
    private AudioSource playerSound;    
    public AudioClip starSound;

    private void Start()
    {
        //Get AudioSource component attached to player
        playerSound = GetComponent<AudioSource>();
    }

    //Adds a specified score value to the tracker
    public void AddStar(int score)
    {
        starCollectible += score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if player collided with a collectible
        if (collision.gameObject.CompareTag("Collectible"))
        {
            playerSound.PlayOneShot(starSound, 0.3f);       //Plays collectible collected SFX
            Destroy(collision.gameObject);                            //Destroys the collectible
            AddStar(_starValue);                                        //Adds 10 (starValue) to the score tracker
        }
    }

}
