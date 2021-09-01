using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Stats")]
    public bool gameEnd = false;            // has the game ended?
    public float timeToWin;                 // time a player needs to hold the hat to win
    public float invincibleDuration;        // how long after a player gets the hat
    private float hatPickupTime;            // the time the hat was picked up by the current player

    [Header("Players")]
    public string playerPrefabLocation;
    public Transform[] spawnPoints;
    public PlayerController[] players;
    public int playerWithHat;
    private int playersInGame;

    // instance
    public static GameManager instance;

    void Awake()
    {
        // instance
        instance = this;
    }

    void Start()
    {
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }

    void ImInGame()
    {
        playersInGame++;

        // when all players are in the scene - spawn the player
        if (playersInGame == PhotonNetwork.PlayerList.Length)
            SpawnPlayer();
    }

    // spawns a player an intializes it
    void SpawnPlayer()
    {
        //  instantiate the player across the network
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

        // get the player script
        PlayerController playerScript = playerObj.GetComponent<PlayerController>();
    }

}
