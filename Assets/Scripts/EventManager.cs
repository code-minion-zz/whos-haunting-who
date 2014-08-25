using UnityEngine;
using System.Collections;

public class EventManager : Photon.MonoBehaviour 
{
    public static EventManager Instance;

    public Transform localPlayer;
    public Transform remotePlayer;
    float nextCheckTime;
    const float delay = 2f;

	// Use this for initialization
	void Start () 
    {
        if (!Instance) Instance = this;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().isMine)
            {
                localPlayer = player.transform;
            }
            else
            {
                remotePlayer = player.transform;
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.time < nextCheckTime) return;

        nextCheckTime = Time.time + delay;
                
        Plane[] planes;
        planes = GeometryUtility.CalculateFrustumPlanes(localPlayer.GetComponentInChildren<Camera>());

        bool looking = GeometryUtility.TestPlanesAABB(planes, remotePlayer.collider.bounds);

        if (looking)
        {
            ShowApparition();
        }
	}

    void ShowApparition()
    {
        PhotonNetwork.Instantiate("Apparition", remotePlayer.position, remotePlayer.rotation);
    }
}
