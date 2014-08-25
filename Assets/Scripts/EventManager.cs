using UnityEngine;
using System.Collections;

public class EventManager : Photon.MonoBehaviour 
{
    public static EventManager Instance;



	// Use this for initialization
	void Start () 
    {
        if (!Instance) Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Activate()
    {
    }
}
