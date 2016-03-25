using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {
	public GameObject enemy;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 0, 1);
	}
	
	void Spawn () {
		Instantiate (enemy, transform.position, Quaternion.identity);
	}
}
