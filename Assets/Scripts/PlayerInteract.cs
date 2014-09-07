using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour {

	public Interactable InterActee;
	public float InteractRate = 20f;

	float CurrentInteractTime;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
		CurrentInteractTime -= Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (InterActee != null)
			{
				InterActee.transform.GetComponent<PhotonView>().RPC("Release", PhotonTargets.AllBufferedViaServer);
				InterActee = null;
			}
			else
			{
				Debug.Log("Casting");
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bool success = Physics.Raycast(ray, out hit, 200f);

				if (success)
				{
					Debug.Log("Hit Found");
					InterActee = hit.transform.GetComponent<Interactable>();
					if (InterActee == null)
					{
						//Never return in an update method.
						//return;
					}
					if (InterActee is Draggable)
					{
						InterActee.transform.GetComponent<PhotonView>().RPC("Capture", PhotonTargets.AllBufferedViaServer);
					}
					//Unused code = possible redundant code. Will it be used in the future?
					//else if (inter is Holdable)
					//{
					//	hit.transform.GetComponent<PhotonView>().RPC("Hold", PhotonTargets.AllViaServer, PhotonView.Get(this).viewID);
					//}

				}
			}
		}

		if (InterActee != null && InterActee is Draggable && CurrentInteractTime <= 0)
		{
			CurrentInteractTime = 1f / InteractRate;
			InterActee.transform.GetComponent<PhotonView>().RPC("UpdatePosition", PhotonTargets.AllBufferedViaServer, Camera.main.transform.position + Camera.main.transform.forward, transform.rotation);
		}
	}
}
