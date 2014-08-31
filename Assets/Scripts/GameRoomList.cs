using UnityEngine;
using System.Collections;

public class GameRoomList : MonoBehaviour 
{
    static string version = "0.1";
    RoomInfo[] roomList = null;

	// Use this for initialization
	void Start () 
    {
        PhotonNetwork.ConnectUsingSettings(version);
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void OnJoinedLobby()
    {
        RefreshRoomList();
    }

    void OnReceivedRoomListUpdate()
    {
        RefreshRoomList();
    }

    void RefreshRoomList()
    {
        roomList = PhotonNetwork.GetRoomList();

        // contact lobby ui handler, clear current list

        foreach (RoomInfo room in roomList)
        {
            // create new room ui thing
            // assign room to that ui
        }
    }
}
