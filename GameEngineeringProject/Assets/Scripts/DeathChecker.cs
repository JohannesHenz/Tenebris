using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.SceneManagement;

public class DeathChecker : MonoBehaviour
{
    private GameObject Player1;
    private GameObject Player2;
    public bool OxygenTimerRanOut;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckPlayersStatus()
    {
        if (OxygenTimerRanOut == true || (Player1.GetComponent<PlayerMovement>().getIsDown() && Player2.GetComponent<PlayerMovement>().getIsDown()))
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }

    public void FindPlayers()
    {
        Player1 = GameObject.Find("Player1");
        Debug.Log("Player1 Found");
        Player2 = GameObject.Find("Player2");
        Debug.Log("Player2 Found");

    }


}
