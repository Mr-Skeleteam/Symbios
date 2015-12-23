using UnityEngine;
using System.Collections;

public class Oxpecker_Control : MonoBehaviour {
	Rigidbody2D rb;
	public int flight;
	bool onBack;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		onBack = false;
		flight = 100;
		InvokeRepeating ("flightReduce",0,0.1f);
	}
	void FixedUpdate () {
		if (GetComponent<Generic_Control> ().isEnabled) {
			rb.AddForce (Vector2.up * Input.GetAxis ("Vertical") * Mathf.Clamp (getFlightStrength (90 - flight),0,50));
			rb.AddForce (Vector2.right * Input.GetAxis ("Horizontal") * Mathf.Clamp (getFlightStrength (90 - flight),0,60));
		}
	}
	float getFlightStrength (int input) {
		return -12 * (Mathf.Pow (1.018079f,input)) + 60;
	}
	void OnCollisionEnter2D (Collision2D other) {
		InvokeRepeating ("flightRegen",0,0.05f);
		CancelInvoke ("flightReduce");
		if (other.gameObject.tag == "Rhino") {
			onBack = true;
		}
	}
	void OnCollisionExit2D (Collision2D other) {
		InvokeRepeating ("flightReduce",0,0.1f);
		CancelInvoke ("flightRegen");
		onBack = false;
	}
	void flightReduce () {
		if (flight > 0) {
			flight--;
		}
	}
	void flightRegen () {
		if (flight < 100) {
			flight++;
		}
	}
	public void onSwitchAway () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position,Vector2.down,0.5f,LayerMask.NameToLayer ("Rhino"));
		Debug.DrawRay (transform.position,Vector2.down);
        if (!hit.Equals (null)) {
			transform.parent = hit.transform;
		}
	}
}
