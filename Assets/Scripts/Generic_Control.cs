using UnityEngine;
using System.Collections;

public class Generic_Control : MonoBehaviour {
	public bool isEnabled = true;
	Rigidbody2D rb;
	Quaternion goal;
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		goal = Quaternion.Euler (0,0,0);
	}

	void FixedUpdate () {	
		int direction = 0; // -1 = left; 1 == right
		if (Input.GetAxis ("Horizontal") != 0) {
			direction = (int) (Input.GetAxis ("Horizontal") / (Mathf.Abs (Input.GetAxis ("Horizontal"))));
			goal = Quaternion.Euler (0,90 - 90 * direction,0);
		}
		transform.rotation = Quaternion.Slerp (transform.rotation,goal,0.3f);
	}

	public bool canSwitch () {
		return (rb.velocity.magnitude <= 0.01f);
	}
}
