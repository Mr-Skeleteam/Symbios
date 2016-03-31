using UnityEngine;
using System.Collections;

public class MissileManager : MonoBehaviour {
	public GameObject missile;
	void Awake () {
		InvokeRepeating ("Launch",3,3);
	}
	void Launch () {
		GameObject misObj = (GameObject) Instantiate (missile, transform.position, Quaternion.identity);
		Missile mis = misObj.GetComponent<Missile> ();
		mis.origin = transform.position;
		mis.direction = Vector2.up;
		mis.target = GameObject.Find ("Rhino").transform;
	}
}
