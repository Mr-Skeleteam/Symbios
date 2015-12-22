using UnityEngine;
using System.Collections;

public class Rhino_Control : MonoBehaviour {
	Rigidbody2D rb;
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	void FixedUpdate () {
		if (GetComponent<Generic_Control> ().isEnabled) {
			rb.AddForce (Vector2.right * Input.GetAxis ("Horizontal") * 5000);
		}
		
	}
}
