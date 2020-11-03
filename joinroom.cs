using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using JetBrains.Annotations;
using Photon.Realtime;

public class joinroom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject opponentpanel = null;
    public GameObject waitingpanel = null;
    public Text waitingtext;
    public bool isconnecting = false;
    private const string gameversion = "0.2";
    private const int maxplayers = 2;
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    public void findopponent()
    {
        isconnecting = true;
        opponentpanel.SetActive(false);
        waitingpanel.SetActive(true);
        waitingtext.text = "searching";

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameversion;
            PhotonNetwork.ConnectUsingSettings();
        }


    }
    public override void OnConnectedToMaster()
    {
        if (isconnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingpanel.SetActive(false);
        opponentpanel.SetActive(false);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxplayers });
    }
    public override void OnJoinedRoom()
    {
        int playercount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playercount != maxplayers)
        {
            waitingtext.text = "waiting for opponent";
        }
        else
        {
            waitingtext.text = "opponent found";
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxplayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            waitingtext.text = "opponent found";
           

            PhotonNetwork.LoadLevel("level1");
        }
    }
}
