using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        GameObject player1 = Instantiate(playerPrefab, new Vector3(-1, 1, 0), Quaternion.identity);
        GameObject player2 = Instantiate(playerPrefab, new Vector3(1, 1, 0), Quaternion.identity);

        player1.GetComponent<PlayerMovement>().horizontalInputAxis = "Horizontal";
        player1.GetComponent<PlayerMovement>().verticalInputAxis = "Vertical";
        player1.GetComponent<Interacter>().interactButton = "Button1P1";
        player1.GetComponent<PlayerMovement>().soundButton = "Button2P1";
        player1.GetComponent<PlayerMovement>().downButton = "TestingKillP1";  // Key to down Player 1

        player2.GetComponent<PlayerMovement>().horizontalInputAxis = "Horizontal2";
        player2.GetComponent<PlayerMovement>().verticalInputAxis = "Vertical2";
        player2.GetComponent<Interacter>().interactButton = "Button1P2";
        player2.GetComponent<PlayerMovement>().soundButton = "Button2P2";
        player2.GetComponent<PlayerMovement>().downButton = "TestingKillP2";  // Key to down Player 2

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
