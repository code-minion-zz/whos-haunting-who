using UnityEngine;
using System.Collections;

public class Holdable : Interactable 
{
	//[RPC]
	//void Hold(int _id)
	//{
	//	Transform _transform = PhotonView.Find(_id).transform;
	//	transform.parent = _transform;
	//	transform.localPosition = Vector3.zero;
	//	target = _transform;
	//	Interacting = true;
	//}

	//[RPC]
	//void Unhold()
	//{
	//	transform.parent = null;
	//	target = null;
	//	Interacting = false;
	//}
}
