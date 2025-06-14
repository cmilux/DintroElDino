using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    private readonly float _xMinRange = -14f;          //Screen range from the left side
    private readonly float _xMaxRange = 150f;          //Screen range from the right side
    private readonly float _yRange = -7f;              //Screen range from the bottom
    private readonly float _endGameRange = 140;

    //Player scripts variables
    private PlayerHealth _playerHealth;
    private PlayerController _playerController;
    private PlayerInventory playerInventory;

    //UI
    public TextMeshProUGUI healthCounter;
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI finalScore;

    //Game over screen
    public GameObject gameOver;
    //Player GameOject
    public GameObject player;

    //Gets scripts components from player
    private void Start()
    {
        //Finds the player scripts
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();

        //Set frame rate
        Application.targetFrameRate = 60;
        //vSync is disabled so it respects targetFrameRate
        QualitySettings.vSyncCount = 0;
    }

    //Checks every frame to see if this methods are called
    void Update()
    {
        SettingText();      //Update UI
        GameOver();         //Restarts or shows results of game

        //Calls RestartGame method if R key is pressed
        if (Input.GetKeyUp(KeyCode.R))
        {
            RestartGame();
        }
    }

    //Keeps the player between certain screen range
    public void PlayerSideScreenLimit()
    {
        //Player is on left side limit
        if (player.transform.position.x < _xMinRange)
        {
            player.transform.position = new Vector2(_xMinRange, transform.position.y);
        }

        //Player is on right side limit
        if (player.transform.position.x > _xMaxRange)
        {
            player.transform.position = new Vector2(_xMaxRange, transform.position.y);
        }
    }

    //Updates the UI with current values
    public void SettingText()
    {
        healthCounter.SetText("Lives: " + _playerHealth.health);
        scoreCounter.SetText("Score: " + playerInventory.starCollectible);
    }

    //Restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");       //Loads the gameScene
        _playerHealth.health = 3;        //Resets lives
        gameOver.SetActive(false);      //The final score is set unactive

    }

    //Game over if lives are gone or if player falls into the void
    public void GameOver()
    {
        //If player has no lives or falls into the void (yRange)
        if (_playerHealth.health <= 0 || _playerController.transform.position.y < _yRange)
        {
            RestartGame();      //Restarts the game automatically
        }

        //If player finish the game
        if (_playerController.transform.position.x >= _endGameRange)
        {
            //Sets the gameOver GameObject active and shows the results
            gameOver.SetActive(true);
            finalScore.SetText("Your score: " + playerInventory.starCollectible);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");       //Loads the menu
    }
}
