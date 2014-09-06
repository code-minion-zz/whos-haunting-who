using UnityEngine;
using System.Collections.Generic;

public class GameRoomList : MonoBehaviour 
{
    public static GameRoomList Instance;
    public GameObject roomPrefab;

    List<GameRoomPanel> inactivePool;
    List<GameRoomPanel> activePool;
    int poolCapacity = 25;


	void Start () 
    {
        if (Instance == null)
        {
            Instance = this;
        }

        activePool = new List<GameRoomPanel>(poolCapacity);
        inactivePool = new List<GameRoomPanel>(poolCapacity);
        for (int i = 0; i < poolCapacity; ++i)
        {
            CreateRoom();
        }
	}

    void CreateRoom()
    {
        GameObject go = (GameObject)Instantiate(roomPrefab);

        go.transform.parent = transform;
        GameRoomPanel grp = go.GetComponent<GameRoomPanel>();
        grp.ResetPanel();
        
        inactivePool.Add(grp);
    }

    void AddRoom(RoomInfo room)
    {
        GameRoomPanel panel = GetPanel();
        activePool.Add(panel);
        panel.Room = room;
    }

    public void RemoveRoom(GameRoomPanel panel)
    {
        panel.ResetPanel();
        activePool.Remove(panel);
        inactivePool.Add(panel);
    }

    public GameRoomPanel GetPanel()
    {
        GameRoomPanel retval;

        if (inactivePool.Count < 1)
        {
            CreateRoom();
        }

        retval = inactivePool[0];
        inactivePool.Remove(retval);

        return retval;
    }

    void OnReceivedRoomListUpdate()
    {
        Debug.Log("OnReceivedRoomListUpdate");

        foreach (GameRoomPanel room in activePool)
        {
            RemoveRoom(room);
        }

        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        foreach (RoomInfo room in rooms)
        {
            AddRoom(room);
        }
    }
}
