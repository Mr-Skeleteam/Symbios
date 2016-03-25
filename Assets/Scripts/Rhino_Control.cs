using UnityEngine;
using System.Collections;

public class Rhino_Control : MonoBehaviour {
	Rigidbody2D rb;
	ParticleSystem land;
	ParticleSystem move;
	public bool canJump;
	public bool isGrounded = false;
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		land = transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
		move = transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();
		canJump = true;
	}
	void FixedUpdate () {
		if (canJump) {
			rb.AddForce (Vector2.right * Input.GetAxis ("Horizontal") * 1000,ForceMode2D.Impulse);
			//rb.MovePosition (transform.position + Input.GetAxis ("Horizontal") * Vector3.right * Time.deltaTime * 10);
			/*
			if (Mathf.Abs (rb.velocity.x) > 5) {
				rb.AddForce (Vector2.right * 500 * (-rb.velocity.x/Mathf.Abs (rb.velocity.x)), ForceMode2D.Impulse);
			}
			*/
			rb.velocity = new Vector2 (Mathf.Clamp (rb.velocity.x,-5f,5f),rb.velocity.y);
			if (Input.GetKeyDown (KeyCode.W)) {
				rb.AddForce (Vector2.up * 1750,ForceMode2D.Impulse);
			}
		}
		if (isGrounded && rb.velocity.sqrMagnitude > 0.01f) {
			if (!move.isPlaying) {
				move.Play ();
			}
		} else {
			if (!move.isStopped) {
				move.Stop ();
			}
		}
	}
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = true;
			StartCoroutine (landCooldown ());
		}
	}
	void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			canJump = false;
			isGrounded = false;
		}
	}
	IEnumerator landCooldown () {
		land.Play ();
		yield return new WaitForSeconds (0.1f);
		canJump = true;
	}
}
