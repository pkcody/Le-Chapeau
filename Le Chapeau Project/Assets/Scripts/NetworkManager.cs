using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // instance
    public static NetworkManager instance;

    private void Awake()
    {
        // if an instance already exists and it's not this one - destroy us
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
            Debug.Log("hi");
        }

        else
        {
            // set the instance
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // connect to the master server
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
    }

    // attempts to CREATE a room
    public void CreateRoom (string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    // attempts to JOIN a room
    public void JoinRoom (string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    // changes the scene using Photon's system
    [PunRPC]
    public void ChangeScene (string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
