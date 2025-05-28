using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private PlayerInventory playerInventory;

    public TextMeshProUGUI healthCounter;
    public TextMeshProUGUI scoreCounter;

    //Gets scripts components from player
    private void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    //Checks every frame to see if this methods are called
    void Update()
    {
        ExitGame_Build();

        GameOver();

        healthCounter.SetText("Lives: " + playerHealth.health);
        scoreCounter.SetText("Score: " + playerInventory.starCollectible);
    }

    //Close the game from the build
    public void ExitGame_Build()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //Restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene("Game Scene");

        playerHealth.health = 3;

    }

    //Game over if lives are gone or if player falls into the void
    public void GameOver()
    {
        if (playerHealth.health <= 0 || playerController.transform.position.y < playerController.yRange)
        {
            Debug.Log("Game Over");
            RestartGame();
        }
        
    }

    //Closes the game from the exit button
    public void ExitFromGame_Unity()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
