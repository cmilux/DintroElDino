using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    //Player script variable
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;

    //Player hurt SFX
    private AudioSource _playerSFX;
    public AudioClip _playerDamageFX;

    private void Start()
    {
        //Gets scripts components from player
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        //Get's the audio source
        _playerSFX = GetComponent<AudioSource>();

        //Get player health script
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Checks if player collided with an obstacle
        if (collision.gameObject.CompareTag("Player"))
        {

            if (_playerHealth != null)       //if player has any lives
            {
                _playerHealth.DamagePlayer();        //Call DamagePlayer()
            }
            //_playerController.playerIsOnGround = true;        LEAVE IN HERE JUST IN CASE
            _playerController.animator.SetBool("IsAlive", false);       //Plays hurt animation
            _playerSFX.PlayOneShot(_playerDamageFX, 0.2f);              //Plays hurt SFX
        }
    }

    //When player stops colliding with the obstacle
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //_playerController.playerIsOnGround = true;        LEAVE IN HERE JUST IN CASE
            _playerController.animator.SetBool("IsAlive", true);      //Stops hurt animation
        }
    }
}
