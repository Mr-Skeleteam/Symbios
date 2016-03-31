using UnityEngine;
using System.Collections;

public class Rhino_Control : MonoBehaviour {
	Rigidbody2D rb;
	ParticleSystem land;
	ParticleSystem move;
	Animator anim;
	public bool canJump;
	public bool isGrounded = false;


	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		land = transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
		move = transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();
		anim = GetComponent<Animator> ();
		canJump = true;
	}
	void Update () {
		if (canJump) {
			rb.AddForce (Vector2.right * Input.GetAxis ("Horizontal") * 1000,ForceMode2D.Impulse);
			rb.velocity = new Vector2 (Mathf.Clamp (rb.velocity.x,-5f,5f),rb.velocity.y);
			if (Mathf.Abs (Input.GetAxis ("Horizontal")) < 0.1f) {
				rb.velocity = new Vector2 (0,rb.velocity.y);
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				rb.AddForce (Vector2.up * 2000,ForceMode2D.Impulse);
			}
		} else {
			rb.AddForce (Vector2.right * Input.GetAxis ("Horizontal") * 10,ForceMode2D.Impulse);
		}
		if (isGrounded && Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.0001f) {
			if (!move.isPlaying) {
				move.Play ();
				anim.SetBool ("IsWalking",true);
			}
		} else {
			if (!move.isStopped) {
				move.Stop ();
				anim.SetBool ("IsWalking",false);
			}
		}
	}
	void OnCollisionEnter2D (Collision2D other) {
		transform.rotation = Quaternion.Euler (new Vector3 (0,transform.rotation.eulerAngles.y,0));
		if (other.gameObject.tag == "Ground") {
			//StartCoroutine (landCooldown ());
			land.Play ();
			foreach (GameObject enemy in GameObject.FindGameObjectsWithTag ("Enemy")) {
				if (Vector3.Distance (enemy.transform.position, transform.position) < 2 && enemy.GetComponent<Enemy_Ai> ().health > 0) {
					enemy.SendMessage ("Knockback", Vector3.up * 100 + Vector3.right * (enemy.transform.position-transform.position).x * 100);
					enemy.SendMessage ("GetStomped");
					enemy.SendMessage ("TakeDamage",5);
				}
			}
		}
		if (other.gameObject.GetComponentInParent<Elevator> () != null && rb.velocity.y < 0) {
			transform.position = new Vector2 (transform.position.x,other.transform.position.y);
		}
	}
	void OnCollisionStay2D (Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = true;
			canJump = true;
		}
		if (other.gameObject.GetComponentInParent<Elevator> () != null) {
			transform.position = new Vector2 (transform.position.x,other.transform.position.y);
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
