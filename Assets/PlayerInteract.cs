using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour {

    public enum PlayerAction
    {
        None,
        Dragging,
        Holding
    }

    PlayerAction currentAction;
    bool canInteract = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!canInteract) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool success = Physics.Raycast(ray, out hit, 2f, LayerMask.NameToLayer("Interactable"));

            if (success)
            {
                hit.transform.GetComponent<Interactable>();
            }
        }
	}
}
