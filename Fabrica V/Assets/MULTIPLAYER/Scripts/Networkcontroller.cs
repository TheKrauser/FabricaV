using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Networkcontroller : MonoBehaviourPunCallbacks
{
    [Header("LOGIN")]
    public GameObject loginPn;
    public InputField playerNameInput;
    string playerTempName;

    [Header("LOBBY")]
    public GameObject lobbyPn;
    public InputField roomNameInput;
    string roomTempName;

    [Header("Player")]
    public GameObject player;


    private void Start()
    {
        loginPn.gameObject.SetActive(true);
        lobbyPn.gameObject.SetActive(false);

        playerTempName = "Anonimo" + Random.Range(0,99);
        playerNameInput.text = playerTempName;

        roomTempName = "NoFear" + Random.Range(1, 99);
    }
    public void Login()
    {
        PhotonNetwork.ConnectUsingSettings();
        if (playerNameInput.text != "")
        {
            PhotonNetwork.NickName = playerNameInput.text;
        }
        else
        {
            PhotonNetwork.NickName = playerTempName;
        }
        loginPn.gameObject.SetActive(false);
        lobbyPn.gameObject.SetActive(true);

        roomNameInput.text = roomTempName;
    }

    public void QuickSearch()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        Debug.LogWarning("OnConnected");
    }
    public void createroom()
    {
        RoomOptions roomoptions = new RoomOptions() { MaxPlayers = 2 };

        if (roomNameInput.text != "")
        {
            PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, roomoptions, TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.JoinOrCreateRoom(roomTempName, roomoptions, TypedLobby.Default);
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.LogWarning("OnConnectedToMaster");
        Debug.LogWarning("Server Region " + PhotonNetwork.CloudRegion);
        Debug.LogWarning("Ping " + PhotonNetwork.GetPing());
    }

    public override void OnJoinedLobby()
    {
        Debug.LogWarning("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("OnJoinRandomFailed");
        createroom();
    }
    public override void OnJoinedRoom()
    {
        Debug.LogWarning("OnJoinedRoom");
        Debug.LogWarning("Room Name: "+PhotonNetwork.CurrentRoom);
        Debug.LogWarning("Count Players: " + PhotonNetwork.CountOfPlayers);
        loginPn.gameObject.SetActive(false);
        lobbyPn.gameObject.SetActive(false);

        PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation);
    }

}
