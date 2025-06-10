using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int starCollectible;

    private AudioSource playerSound;
    public AudioClip starSound;

    private void Start()
    {
        playerSound = GetComponent<AudioSource>();
    }

    //Adds 10 everytime a collectible is picked
    public void AddStar(int score)
    {
        starCollectible += score;
    }


    //Collects the collectible, destroys it and add 10 everytime
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            playerSound.PlayOneShot(starSound, 0.3f);
            Destroy(collision.gameObject);
            AddStar(10);
        }
    }

}
