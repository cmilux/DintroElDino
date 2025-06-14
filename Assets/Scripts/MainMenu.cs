using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        //Calls ExitFromGame method if escape key is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Exit();
        }
    }
    //If player clicks on start button, game starts
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");        //Loads the game scene
    }

    //If player clicks on exit button, player exits the game
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}
