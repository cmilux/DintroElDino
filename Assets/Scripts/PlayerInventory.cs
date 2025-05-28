using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int starCollectible;

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
            Destroy(collision.gameObject);
            AddStar(10);
        }
    }

}
