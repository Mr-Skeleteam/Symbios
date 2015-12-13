using UnityEngine;
using System.Collections;

public class Oxpecker_Control : MonoBehaviour {
	Rigidbody2D rb;
	public int flight;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		flight = 100;
		InvokeRepeating ("flightReduce",0,0.1f);
	}
	void FixedUpdate () {
		rb.AddForce (Vector2.up * Input.GetAxis ("Vertical") * Mathf.Clamp (getFlightStrength (90 - flight),0,50));
		rb.AddForce (Vector2.right * Input.GetAxis ("Horizontal") * Mathf.Clamp (getFlightStrength (90 - flight),0,30));
	}
	float getFlightStrength (int input) {
		return -12 * (Mathf.Pow (1.018079f,input)) + 60;
	}
	void OnCollisionEnter2D (Collision2D other) {
		InvokeRepeating ("flightRegen",0,0.05f);
		CancelInvoke ("flightReduce");
	}
	void OnCollisionExit2D (Collision2D other) {
		InvokeRepeating ("flightReduce",0,0.1f);
		CancelInvoke ("flightRegen");
		Debug.Log ("ayy");
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
}
