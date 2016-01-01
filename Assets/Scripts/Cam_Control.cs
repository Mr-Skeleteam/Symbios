using UnityEngine;
using System.Collections;

public class Cam_Control : MonoBehaviour {
	public GameObject obj;
	Rigidbody2D rb;
	void Awake () {
		rb = obj.GetComponent<Rigidbody2D> ();
	}
	void LateUpdate () {
		Vector3 end = obj.transform.position + Vector3.forward * -10;
		Vector3 v = rb.velocity * 0.25f;
		v.y *= 0.5f;
		transform.position = Vector3.Lerp (transform.position,end + v,0.05f);
	}
}
