using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour 
{
    public enum Interaction
    {
        None,
        Drag,
        Hold,
    }
    public Interaction currentInteration = Interaction.None;
    protected Transform target;
    protected Vector3 previousPosOfTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () 
    {
        if (currentInteration == Interaction.Drag)
        {
            Vector3 diff = target.position - transform.position;
            //previousPosOfTarget = target.position;
            Debug.Log("Dragging Velo " + diff);

            transform.rigidbody.AddForce(diff, ForceMode.VelocityChange);
            if (diff.magnitude > 1)
            {
                target = null;
                currentInteration = Interaction.None;
            } 
        }
	}


    [RPC]
    void Hold(Transform _transform)
    {
        transform.parent = _transform;
        transform.localPosition = Vector3.zero;
    }

    [RPC]
    void Unhold()
    {
        transform.parent = null;
    }
}
