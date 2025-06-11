using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    //Player scripts variables
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private PlayerInventory playerInventory;

    //UI
    public TextMeshProUGUI healthCounter;
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI finalScore;

    //Game over screen
    public GameObject gameOver;

    //Gets scripts components from player
    private void Start()
    {
        //Finds the player scripts
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    //Checks every frame to see if this methods are called
    void Update()
    {
        SettingText();      //Update UI
        ExitGame_Build();   //Close the game with escape key
        GameOver();         //Restarts or shows results of game
    }

    //Updates the UI with current values
    public void SettingText()
    {
        healthCounter.SetText("Lives: " + playerHealth.health);
        scoreCounter.SetText("Score: " + playerInventory.starCollectible);
    }

    //Close the game from the build if player press Esc
    public void ExitGame_Build()
    {
        if (Input.GetKeyDown(KeyCode.Escape))       //Player input
        {
            Application.Quit();     //Action (Quit the game)
        }
    }

    //Restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene("Game Scene");       //Loads the gameScene

        playerHealth.health = 3;        //Resets lives

        gameOver.SetActive(false);      //The final score is set unactive

    }

    //Game over if lives are gone or if player falls into the void
    public void GameOver()
    {
        //If player has no lives or falls into the void (yRange)
        if (playerHealth.health <= 0 || playerController.transform.position.y < playerController.yRange)
        {
            RestartGame();      //Restarts the game automatically

            Debug.Log("Game Over");
        }

        //If player finish the game
        if (playerController.transform.position.x >= 140)
        {
            //Sets the gameOver GameObject active and shows the results
            gameOver.SetActive(true);
            finalScore.SetText("Your score: " + playerInventory.starCollectible);
        }


    }

    //Closes the game from editor when the exit button is clicked
    public void ExitFromGame_Unity()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }

}
