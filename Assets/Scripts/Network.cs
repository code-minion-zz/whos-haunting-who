﻿using UnityEngine;
using System.Collections.Generic;

public class Network : MonoBehaviour 
{
    List<Transform> remotePlayers;

	// Use this for initialization
	void Start () 
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	// Update is called once per frame
	void Update () 
    {
        	
	}

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();

    }

    void OnJoinedRoom()
    {
        GameObject go = PhotonNetwork.Instantiate("GamePlayer", Vector3.zero, Quaternion.identity, 0);
        go.GetComponent<vp_FPController>().enabled = true;
        go.GetComponent<vp_FPInput>().enabled = true;
        go.transform.FindChild("FPSCamera").gameObject.SetActive(true);
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }
}