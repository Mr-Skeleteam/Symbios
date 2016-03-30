using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	Rigidbody2D rb;
	public int damage;
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	void Update () {
		if (Vector3.Distance (transform.position, Camera.main.transform.position) > 20) {
			Destroy (gameObject);
		}
	}
	void Launch (Vector3 Direction) {
		rb.AddForce (Direction.normalized * 10,ForceMode2D.Impulse);
		float spinDirection = Direction.normalized.x;
		rb.AddTorque (-spinDirection * 10,ForceMode2D.Impulse);
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "PlayerHitbox") {
			other.transform.parent.gameObject.SendMessage ("TakeDamage",damage);
			Destroy (gameObject);
		}
	}
}
