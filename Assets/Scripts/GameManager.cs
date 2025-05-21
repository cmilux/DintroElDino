using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        ExitGame_Build();


        GameOver();

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

    //Game over
    public void GameOver()
    {
        if (playerHealth.health == 0)
        {
            Debug.Log("Game Over");
            RestartGame();
        }
    }

    //Closes the game from a button
    public void ExitFromGame_Unity()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
