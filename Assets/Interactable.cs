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
    Transform target;
    Vector3 previousPosOfTarget;
    Vector3 offsetFromTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (currentInteration == Interaction.Drag)
        {
            Vector3 diff = target.position - previousPosOfTarget;
            if (diff.magnitude < 0.05) return;
            transform.position = transform.position + diff;
        }
	}

    [RPC]
    void Grab(Transform _transform)
    {
        target = _transform;
        previousPosOfTarget = target.position;
        offsetFromTarget = transform.position - target.position;
    }
}
