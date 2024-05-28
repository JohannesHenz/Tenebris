using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void RetryGame()
    {
        Debug.Log("Reloading MainScene");
        SceneManager.LoadScene("MainScene");
    }

    public void GoBackToTitleScreen()
    {
        Debug.Log("Return to TitleScreen");
        SceneManager.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
