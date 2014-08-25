using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpringJoint))]
public class Draggable : Interactable 
{
    SpringJoint joint;
    
	// Use this for initialization
	void Start () 
    {
        joint = GetComponent<SpringJoint>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();
	}

    [RPC]
    void Grab(int _id)
    {
        PhotonView view = PhotonView.Find(_id);
        Transform _transform = view.transform;
        Debug.Log("DRAG INITIATED");
        target = _transform;
        previousPosOfTarget = target.position;
        currentInteration = Interaction.Drag;
    }
}
