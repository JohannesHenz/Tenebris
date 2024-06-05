using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    private List<GameObject> spawnedPlayers = new List<GameObject>();



    void OnEnable()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    void OnDisable()
    {
        PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        // Check if it's the first player or second player to join and set positions accordingly
        Vector3 basePosition = transform.position;
        Vector3 spawnPosition = basePosition + new Vector3(-1 + spawnedPlayers.Count * 2, 1.25f, 0);
        playerInput.gameObject.transform.position = spawnPosition;
        spawnedPlayers.Add(playerInput.gameObject);
    }

    void JoinPlayer()
    {

    }
}
