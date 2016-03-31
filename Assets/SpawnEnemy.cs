using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {
	public GameObject enemy;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 0, 1);
	}
	
	void Spawn () {
		if (!Camera.main.GetComponent<MainMenu> ().inLoad) Instantiate (enemy, transform.position + Vector3.right * ((Random.value * 2)-1) * 10, Quaternion.identity);
	}
}
