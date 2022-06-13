using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerSetup : MonoBehaviourPunCallbacks
{
   [SerializeField] Camera fpsCamera;
    private void Start()
    {
        if (!photonView.IsMine)
        {
            transform.GetComponent<PlayerController>().enabled = false;
            transform.GetComponentInChildren<GroundChecker>().enabled = false;
            fpsCamera.enabled = false;
        }
        else
        {
            transform.GetComponent<PlayerController>().enabled = true;
            transform.GetComponentInChildren<GroundChecker>().enabled = true;
            fpsCamera.enabled = true;
        }
       
    }
}
