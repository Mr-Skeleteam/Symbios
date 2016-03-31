using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {
	public GameObject Background;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnBack",0,5);
		
	}
	
	// Update is called once per frame
	void SpawnBack () {
		Instantiate (Background,Camera.main.transform.position + Vector3.forward * 10 + transform.right * 40,Quaternion.identity);
	}
}
