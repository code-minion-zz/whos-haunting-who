using UnityEngine;
using System;
using System.Collections;

public class Draggable : Interactable 
{
    Vector3 correctPos;
    Quaternion correctRot;

  	// Use this for initialization
	void Start () 
    {
        correctPos = transform.position;
		correctRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, correctPos, Time.deltaTime * 8);
		transform.rotation = Quaternion.Lerp(transform.rotation, correctRot, Time.deltaTime * 8);

		if (IsUsed)
		{
			transform.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			transform.GetComponent<BoxCollider>().enabled = true;
		}
	}

	[RPC]
	void Capture()
	{
		IsUsed = true;
	}

	[RPC]
	void Release()
	{
		IsUsed = false;
	}

	[RPC]
	void UpdatePosition(Vector3 Position, Quaternion Rotation)
	{
		if (IsUsed)
		{
			correctPos = Position;
			correctRot = Rotation;
			//transform.position = Position;
			//transform.rotation = Rotation;
			Debug.Log("Updated position of " + transform.name + " to: " + Position.ToString() + " | " + Rotation.eulerAngles.ToString());
		}
		else
		{
			throw new InvalidOperationException("You should not update the position if the object isn't used.");
		}
	}
}
