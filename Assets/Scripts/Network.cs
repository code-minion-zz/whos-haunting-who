using UnityEngine;
using System.Collections;

public class Network : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	// Update is called once per frame
	void Update () {
	
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
        PhotonNetwork.Instantiate("RemotePlayer", Vector3.zero, Quaternion.identity, 0);
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }
}
