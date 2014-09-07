using UnityEngine;
using System.Collections;

public class NetworkLobby : MonoBehaviour 
{
    public UIInput createGameName;
    public UIButton createGameButton;
    public static NetworkLobby Instance;

	// Use this for initialization
	void Start ()
    {
        if (Instance == null) Instance = this;

        createGameButton.isEnabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {	
	}

    public void Connect(RoomInfo room)
    {
        if (PhotonNetwork.connecting || PhotonNetwork.connected) return;

        PhotonNetwork.JoinRoom(room.name);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createGameName.value, true, true, 2);
    }
}
