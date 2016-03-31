using UnityEngine;
using System.Collections;

public class Cam_Control : MonoBehaviour {
	GameObject obj;
	Rigidbody2D rb;
	void Awake () {
	}
	void Update () {
		if (GameObject.Find ("Rhino") != null) {
			obj = GameObject.Find ("Rhino");
			transform.position = obj.transform.position + Vector3.forward * -10;
		}
	}
}
