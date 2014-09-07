using UnityEngine;
using System.Collections.Generic;

public class GameRoomList : MonoBehaviour 
{
    public static GameRoomList Instance;
    public GameObject roomPrefab;
    UIGrid grid;

    List<GameRoomPanel> inactivePool;
    List<GameRoomPanel> activePool;
    int poolCapacity = 25;


	void Start () 
    {
        if (Instance == null)
        {
            Instance = this;
        }

        grid = GetComponent<UIGrid>();
        activePool = new List<GameRoomPanel>(poolCapacity);
        inactivePool = new List<GameRoomPanel>(poolCapacity);
        for (int i = 0; i < poolCapacity; ++i)
        {
            CreatePanel();
        }
	}

    void CreatePanel()
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
        //NGUITools.SetActive(panel.gameObject, true);
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
            CreatePanel();
        }

        retval = inactivePool[0];
        inactivePool.Remove(retval);

        return retval;
    }

    public void RefreshServerList()
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

        grid.repositionNow = true;
    }

    void OnReceivedRoomListUpdate()
    {
        RefreshServerList();
    }
}
