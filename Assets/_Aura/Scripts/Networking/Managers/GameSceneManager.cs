using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class GameSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField]TMP_Text debugText;
    //will run when master client joins room
   
    public override void OnJoinedRoom()
    {
        debugText.text = PhotonNetwork.NickName + " joined GameScene!";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        debugText.text = newPlayer.NickName + " joined GameScene!";
    }

}
