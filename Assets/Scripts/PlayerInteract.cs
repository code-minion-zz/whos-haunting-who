using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour 
{

    public enum PlayerAction
    {
        None,
        Dragging,
        Holding
    }

    public PlayerAction currentAction;
    Transform objectInHand;
//    bool canInteract = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        switch (currentAction)
        {
            case PlayerAction.Holding:

                break;

            default:

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Casting");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool success = Physics.Raycast(ray, out hit, 200f);

                if (success)
                {
                    Debug.Log("Hit Found");
                    Interactable inter = hit.transform.GetComponent<Interactable>();
                    if (inter == null) return;
                    if (inter is Draggable)
                    {
                        currentAction = PlayerAction.Dragging;
                        objectInHand = hit.transform;
                        hit.transform.GetComponent<PhotonView>().RPC("Grab", PhotonTargets.AllViaServer, PhotonView.Get(this).viewID);                        
                    }
                    else if (inter is Holdable)
                    {
                        currentAction = PlayerAction.Holding;
                        objectInHand = hit.transform;
                        hit.transform.GetComponent<PhotonView>().RPC("Hold", PhotonTargets.AllViaServer, PhotonView.Get(this).viewID);
                    }
                 
                }
            }
            break;
        }
	}
}
