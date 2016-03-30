using UnityEngine;
using System.Collections;

public class Enemy_Ai : MonoBehaviour {
	public GameObject Projectile;
	GameObject player;
	Rigidbody2D rb;
	public int sight = 10;
	public float speed = .1f;
	bool grounded = false;
	public int health;
	public int maxHealth;
	bool alive;
	bool inAttack = false;
	
	public enum State {
		IDLE,
		ACTIVE
	};
	
	public void Awake () {
		health = maxHealth;
		alive = true;
		player = GameObject.Find ("Rhino");
		rb = GetComponent<Rigidbody2D> ();
	}
	
	public State currentState = State.IDLE;
	
	public void FixedUpdate () {
		if (alive) {
			switch (currentState) {
				case State.IDLE:
					if (Vector3.Distance (player.transform.position, transform.position) <= sight) {
						currentState = State.ACTIVE;
					}
						break;
				case State.ACTIVE:
					//TODO add activity
					if (grounded) {
						Vector2 flatDirectionToPlayer = player.transform.position - transform.position;
						flatDirectionToPlayer.y = 0;
						flatDirectionToPlayer.Normalize ();
						if (Vector3.Distance (transform.position, player.transform.position) > 5) {
							rb.AddForce (flatDirectionToPlayer * 2000);
							rb.velocity = new Vector2 (Mathf.Clamp (rb.velocity.x, -speed, speed), rb.velocity.y);
						} else {
							if (!inAttack) {
								Attack ();
							}
						}
					}
					Quaternion finalRot = Quaternion.identity;
					int direction = 0; // -1 = left; 1 = right
					if (Mathf.Abs (rb.velocity.x) > 0.01f) {
						direction = (int) (rb.velocity.x / (Mathf.Abs (rb.velocity.x)));
						finalRot = Quaternion.Euler (0,90 - 90 * direction,0);
					}
					transform.rotation = finalRot;
					if (Vector3.Distance (player.transform.position, transform.position) > sight) {
						currentState = State.IDLE;
					}
					break;
				default:
					print("ew");
					break;
			}
		}
	}
	
	public void OnCollisionEnter2D (Collision2D other) {
		grounded = other.gameObject.tag == "Ground";
	}
	public void OnCollisionExit2D (Collision2D other) {
		grounded = other.gameObject.tag == "Ground";
	}
	
	public void TakeDamage (int damage) {
		health -= damage;
		if (health <= 0) {
			Die ();
		}
	}
	public void Knockback (Vector3 force) {
		if (alive) {
			rb.velocity = new Vector2 (0, rb.velocity.y);
			rb.AddForce (force, ForceMode2D.Impulse);
		}
	}
	void Die () {
		alive = false;
		GetComponent<BoxCollider2D> ().enabled = false;
		transform.GetComponentInChildren <BoxCollider2D> ().enabled = false;
		rb.AddForce (Vector3.up * 300, ForceMode2D.Impulse);
		StartCoroutine (spin ());
	}
	
	IEnumerator spin () {
		reddify ();
		while (transform.position.y > -10) {
			transform.Rotate (0, 20, 0);
			yield return new WaitForEndOfFrame ();
		}
		Destroy (gameObject);
	}
	
	void reddify () {
		GetComponent<SpriteRenderer> ().color = Color.red;
	}

	void Attack () { 
		GameObject proj = (GameObject) Instantiate (Projectile,transform.position,Quaternion.identity);
		proj.SendMessage ("Launch",(player.transform.position + Vector3.up * ((Random.value * 2) - 1)) - transform.position);
		StartCoroutine (AttackCooldown ());
	}

	IEnumerator AttackCooldown () {
		inAttack = true;
		yield return new WaitForSeconds (2);
		inAttack = false;
	}

	bool CloseToAllies (float distance) {
		bool result = false;
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Enemy")) {
			if (Vector3.Distance (obj.transform.position, transform.position) < distance) {
				result = true;
				break;
			}
		}
		return result;
	}
}
