using UnityEngine;
using System.Collections;

public class Draggable : Interactable 
{
    Vector3 correctPos;
    Quaternion correctRot;
    protected Vector3 previousPosOfTarget;
    protected float LeashDistance = 0.5f;
    protected float BreakDistance = 2f;
    
	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Interacting)
        {
            Vector3 diff = target.position - transform.position;

            if (diff.magnitude > LeashDistance)
            {
                transform.rigidbody.AddForce(diff, ForceMode.VelocityChange);
            }
            if (diff.magnitude > BreakDistance)
            {
                photonView.RPC("Snap", PhotonTargets.All);
            }
        }
        else if (!Interacting)
        {
            if (!photonView.isMine)
            {
                transform.position = Vector3.Lerp(transform.position, correctPos, Time.deltaTime * 5);
                transform.rotation = Quaternion.Lerp(transform.rotation, correctRot, Time.deltaTime * 5);
            }
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!Interacting)
        {
            if (stream.isWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);

            }
            else
            {
                // Network player, receive data
                correctPos = (Vector3)stream.ReceiveNext();
                correctRot = (Quaternion)stream.ReceiveNext();

            }
        }
    }

    [RPC]
    void Grab(int _id)
    {
        PhotonView view = PhotonView.Find(_id);
        Transform _transform = view.transform;
        Debug.Log("DRAG INITIATED");
        target = _transform;
        previousPosOfTarget = target.position;
        Interacting = true;
    }

    [RPC]
    void Snap()
    {
        Debug.Log("SNAP!");
        target = null;
        Interacting = false;
    }
}
