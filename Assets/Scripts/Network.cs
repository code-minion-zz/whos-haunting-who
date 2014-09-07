using UnityEngine;
using System.Collections.Generic;

public class Network : MonoBehaviour 
{
    PhotonPlayer myPlayer;
    PhotonPlayer otherPlayer;
    float nextUpdate;
    float delay = 5f;

	// Use this for initialization
	void Start () 
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");

		PhotonNetwork.sendRate = 30;
		PhotonNetwork.sendRateOnSerialize = 30;

        Physics.IgnoreLayerCollision(8, 8);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.time < nextUpdate)
        {
            PhotonNetwork.GetRoomList();

            nextUpdate = Time.time + delay;
        }
	}

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.inRoom)
        {
            GUILayout.Label(PhotonNetwork.room.playerCount.ToString());            
        }
    }


    void OnPhotonPlayerConnected(PhotonPlayer player)
    {

    }
    
    //void OnJoinedRoom()
    //{
    //    GameObject go = PhotonNetwork.Instantiate("GamePlayer2", new Vector3(19,1,19), Quaternion.identity, 0);
    //    go.GetComponent<vp_FPController>().enabled = true;
    //    go.GetComponent<vp_FPInput>().enabled = true;
    //    go.GetComponent<PlayerInteract>().enabled = true;
    //    go.transform.FindChild("FPSCamera").gameObject.SetActive(true);
    //}

    //void OnPhotonRandomJoinFailed()
    //{
    //    PhotonNetwork.CreateRoom(null);
    //}
}
