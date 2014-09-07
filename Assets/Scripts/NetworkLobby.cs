using UnityEngine;
using System.Collections;

public class NetworkLobby : MonoBehaviour
{
    public static NetworkLobby Instance;
    public UIInput createGameName;
    public UIButton createGameButton;
    public UILabel StatusBox;
    UILabel buttonLabel;

	// Use this for initialization
	void Start ()
    {
        if (Instance == null) Instance = this;

        createGameButton.isEnabled = false;
        buttonLabel = createGameButton.GetComponentInChildren<UILabel>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (PhotonNetwork.inRoom)
        {
            buttonLabel.text = "Disconnect";

            createGameName.active = false;
        }
        else
        {
            buttonLabel.text = "Create Game";

            createGameName.active = true;
        }

        StatusBox.text = PhotonNetwork.connectionStateDetailed.ToString();
	}

    public void Connect(RoomInfo room)
    {
        if (PhotonNetwork.connecting || PhotonNetwork.connected) return;

        PhotonNetwork.JoinRoom(room.name);
    }

    public void OnCreateDisconnectButtonPress()
    {
        if (!PhotonNetwork.inRoom)
        {
            CreateRoom();
        }
        else
        {
            LeaveRoom();
        }
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.cleanupCacheOnLeave = true;
        options.maxPlayers = 2;
        PhotonNetwork.CreateRoom(createGameName.value, options, TypedLobby.Default);
        GameRoomList.Instance.RefreshServerList();
    }

    void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void OnJoinedLobby()
    {
        createGameButton.isEnabled = true;
    }
}
