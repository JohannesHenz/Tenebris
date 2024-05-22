using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject deathScreenMenuUI;
    public GameObject gameManager;



    // Update is called once per frame
    void Update()
    {
        
    }

    public void RetryGame()
    {
        Debug.Log("Reloading Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        gameManager.GetComponent<Timer>().ResetTimer();
    }

}
