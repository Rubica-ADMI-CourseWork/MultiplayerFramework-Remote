using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class GameSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text debugText;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float minXSpawnPos, maxXSpawnPos, minZSpawnPos, maxZSpawnPos;
    private void Awake()
    {
     

        if (PhotonNetwork.IsConnected)
        {
            if(playerPrefab != null)
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(minXSpawnPos, maxXSpawnPos), 2f, Random.Range(minZSpawnPos, maxZSpawnPos)), Quaternion.identity);
        }
    }
    public override void OnJoinedRoom()
    {
        debugText.text = PhotonNetwork.NickName + " joined GameScene!";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        debugText.text = newPlayer.NickName + " joined GameScene!";
    }

}
