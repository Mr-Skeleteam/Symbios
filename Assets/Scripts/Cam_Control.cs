using UnityEngine;
using System.Collections;

public class Cam_Control : MonoBehaviour {
	public GameObject obj;
	Rigidbody2D objRb;
	Rigidbody2D rb;
	void Awake () {
		objRb = obj.GetComponent<Rigidbody2D> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	void FixedUpdate () {
		Vector3 end = obj.transform.position + Vector3.forward * -10;
		Vector3 v = objRb.velocity;
		v.y *= 0.5f;
		rb.AddForce (((obj.transform.position + Vector3.forward * -10 + v * 0.05f) - transform.position) * 10);
		//transform.position = Vector3.Lerp (transform.position,end + v * 0.01f,.5f);
	}
}
