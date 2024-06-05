using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private List<GameObject> spawnedPlayers = new List<GameObject>();

    void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
        PlayerInputManager.instance.EnableJoining();
    }

    void OnDestroy()
    {
        if (PlayerInputManager.instance != null)
        {
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        // Set positions for new players
        Vector3 basePosition = transform.position;
        Vector3 spawnPosition = basePosition + new Vector3(-1 + spawnedPlayers.Count * 2, 1.25f, 0);
        playerInput.gameObject.transform.position = spawnPosition;

        // Assign unique name to the player
        playerInput.gameObject.name = "Player" + (spawnedPlayers.Count + 1);

        spawnedPlayers.Add(playerInput.gameObject);
        
        if (spawnedPlayers.Count == 2)
        {
            DeathChecker deathChecker = GameObject.Find("GameManager").GetComponent<DeathChecker>();
            deathChecker.FindPlayers();

        }
        
        Debug.Log("Player " + spawnedPlayers.Count + " added");
    }

    public void ManualJoinPlayer()
    {
        if (spawnedPlayers.Count < 2)  // Adjust the condition based on your max player count
        {
            // Manually join the player
            Debug.Log("Adding new Player manually");
            int nextPlayerIndex = PlayerInputManager.instance.playerCount;
            PlayerInputManager.instance.JoinPlayer(playerIndex: nextPlayerIndex, controlScheme: "Keyboard&Mouse");
        }
    }
}
