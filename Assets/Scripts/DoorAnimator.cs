using UnityEngine;
using System.Collections;

public class DoorAnimator : MonoBehaviour {

	Animator target;

	bool doorActive;

	// Use this for initialization
	void Start () {
		target = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (doorActive) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				ActivateDoor ();
			}
		}
	}

	void ActivateDoor () {
		bool doorOpen = target.GetBool ("Open");
		target.SetBool ("Open", !doorOpen);
		Debug.Log ("DOOR ACTIVATED");
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Player")) {
			doorActive = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.CompareTag ("Player")) {
			doorActive = false;
		}
	}
}
