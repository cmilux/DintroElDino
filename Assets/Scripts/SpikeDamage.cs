using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    //Player script variable
    private PlayerController _playerController;

    private void Start()
    {
        //Gets scripts components from player
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    /*
     *
     *  DOUBLE JUMP PROBLEM HERE IF BOTH (ENTER AND EXIT) ARE TRUE
     * 
     */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Checks if player collided with an obstacle
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.playerIsOnGround = false;
            _playerController.animator.SetBool("IsAlive", false);     //Plays hurt animation
        }
    }

    //When player stops colliding with the obstacle
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.playerIsOnGround = true;
            _playerController.animator.SetBool("IsAlive", true);      //Stops hurt animation
        }
    }
}
