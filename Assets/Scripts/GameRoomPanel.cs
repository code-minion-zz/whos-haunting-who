using UnityEngine;
using System.Collections;

public class GameRoomPanel : MonoBehaviour 
{    
    RoomInfo roomInfo;
    UILabel label;
    UIButton button;
    public RoomInfo Room
    {
        get {return roomInfo;}
        set
        {
            roomInfo = value;

            NGUITools.SetActive(gameObject, true);
        }
    }

    void Awake()
    {
        if (label == null)
        {
            label = GetComponentInChildren<UILabel>();
        }
        if (button == null)
        {
            button = GetComponentInChildren<UIButton>();
        }
    }
    	
	void Update () 
    {
        if (roomInfo == null) return;

        label.text = roomInfo.name;
	}

    //void UpdateRoom()
    //{
    //    if (roomInfo.removedFromList)
    //    {
    //        GameRoomList.Instance.RemoveRoom(this);
    //    }
    //}

    public void ResetPanel()
    {
        roomInfo = null;
        label.text = "";

        NGUITools.SetActive(gameObject, false);
    }

    public void Connect()
    {
        NetworkLobby.Instance.Connect(roomInfo);
    }
}
