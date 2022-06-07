using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    [Header("New room properties")]
    [SerializeField] int maxPlayers;
    [SerializeField] string roomName;
    RoomOptions roomOptions;

    [Header("UI elements")]
    public GameObject LoginPanel;
    public GameObject LobbyPanel;

    #region Monobehaviour Callbacks
    private void Awake()
    {
        InitRoomOptions();
    }
    private void Start()
    {
        LobbyPanel.SetActive(false);
    } 

    #endregion

    #region PUN Callbacks
    public override void OnConnected()
    {
        Debug.Log("Connected to Internet");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected to Photon Servers.");
        LobbyPanel.SetActive(true);
        LoginPanel.SetActive(false);    
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined Room " + PhotonNetwork.CurrentRoom.Name);
    }

    //called when remote player enters the room we are in
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined Room " + PhotonNetwork.CurrentRoom.Name + " "+PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion

    #region Public Utility Methods
    public void ConnectToServerWithName(string name)
    {
        PhotonNetwork.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region Private Methods
    private void InitRoomOptions()
    {
        roomOptions = new RoomOptions()
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = (byte)maxPlayers
        };
    }

    #endregion
}
