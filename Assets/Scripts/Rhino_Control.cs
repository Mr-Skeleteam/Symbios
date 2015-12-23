using UnityEngine;
using System.Collections;

public class Rhino_Control : MonoBehaviour {
	Rigidbody2D rb;
	ParticleSystem land;
	public bool canJump;
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		land = transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();
		canJump = true;
	}
	void FixedUpdate () {
		if (GetComponent<Generic_Control> ().isEnabled) {
			if (canJump) {
				rb.AddForce (Vector2.right * Input.GetAxis ("Horizontal") * 500,ForceMode2D.Impulse);
				float y = rb.velocity.y;
				rb.velocity = new Vector2 (Mathf.Clamp (rb.velocity.x,-10,10),y);
			}
		}
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.W) && canJump && GetComponent<Generic_Control> ().isEnabled) {
			rb.AddForce (Vector2.up * 2500,ForceMode2D.Impulse);
		}
	}
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			StartCoroutine ("landCooldown");
		}
	}
	void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			canJump = false;
		}
	}
	IEnumerator landCooldown () {
		land.Play ();
		yield return new WaitForSeconds (0.5f);
		canJump = true;
	}
}
