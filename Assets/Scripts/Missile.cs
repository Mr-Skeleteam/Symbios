using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	Rigidbody2D rb;
	public Vector2 origin;
	public Vector2 direction;
	public float speed;

	public Transform target;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = direction.normalized * speed; //set speed to the value specified in the direction specified
		transform.position = origin;
	}

	void FixedUpdate () {
		//changes target position from world to local
		Vector3 relPoint = transform.InverseTransformPoint (target.position);

		//If it is to one side, add a force towards it perpendicular to the current velocity
		if (relPoint.y > 0) {
			//By defaut, it will travel along its local x axis, so the y axis will be its sides
			rb.AddForce (transform.up * 10);
		} else if (relPoint.y < 0) {
			rb.AddForce (-transform.up * 10);
		}

		//Have the x axis face the velocity vector
		Vector3 dir = rb.velocity;
		float angle = Mathf.Atan2 (dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle,Vector3.forward);

		//Keep object at a constant velocity
		rb.velocity = rb.velocity.normalized * speed;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "PlayerHitbox") {
			other.transform.parent.SendMessage ("TakeDamage",5);
			Destroy (gameObject);
		}
	}
}
