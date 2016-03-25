using UnityEngine;
using System.Collections;

public class Generic_Control : MonoBehaviour {
	public bool isEnabled = true;
	Rigidbody2D rb;
	Quaternion goal;
	public int health;
	public int maxHealth;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		goal = Quaternion.Euler (0,0,0);
		health = maxHealth;
	}

	void FixedUpdate () {	
		int direction = 0; // -1 = left; 1 = right
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.01f) {
			direction = (int) (Input.GetAxis ("Horizontal") / (Mathf.Abs (Input.GetAxis ("Horizontal"))));
			goal = Quaternion.Euler (0,90 - 90 * direction,0);
		}
		transform.rotation = Quaternion.Slerp (transform.rotation,goal,0.3f);
	}

	public bool canSwitch () {
		return (rb.velocity.magnitude <= 0.01f);
	}
	
	public void TakeDamage (int Damage) {
		if (health > 0) {
			health -= Damage;
		}
	}
}
