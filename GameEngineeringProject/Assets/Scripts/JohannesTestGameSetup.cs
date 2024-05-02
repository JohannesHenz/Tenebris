using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject playerPrefab;
    

    void Start()
    {
        //Spawn the Players relative to the position of the PlayerSpawner Object
        Vector3 basePosition = gameObject.transform.position;
        Vector3 player1Position = basePosition + new Vector3(-1, 1.25f, 0);  // Offset for player 1
        Vector3 player2Position = basePosition + new Vector3(1, 1.25f, 0);  // Offset for player 2

        // Instantiate players at their respective positions
        GameObject player1 = Instantiate(playerPrefab, player1Position, Quaternion.identity);
        GameObject player2 = Instantiate(playerPrefab, player2Position, Quaternion.identity);

        //Set Player 1 Keys
        player1.GetComponent<PlayerMovement>().horizontalInputAxis = "Horizontal";
        player1.GetComponent<PlayerMovement>().verticalInputAxis = "Vertical";
        player1.GetComponent<Interacter>().interactButton = "Button1P1";
        player1.GetComponent<PlayerMovement>().soundButton = "Button2P1";

        //Set Player 2 Keys
        player2.GetComponent<PlayerMovement>().horizontalInputAxis = "Horizontal2";
        player2.GetComponent<PlayerMovement>().verticalInputAxis = "Vertical2";
        player2.GetComponent<Interacter>().interactButton = "Button1P2";
        player2.GetComponent<PlayerMovement>().soundButton = "Button2P2";

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
