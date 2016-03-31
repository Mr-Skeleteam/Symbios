using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	Rigidbody2D rb;
	public bool up = false; //initial direction of movement
	public float distance;
	Vector3[] initialPos;
	float distTravelled = 0;
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		initialPos = new Vector3[transform.childCount];
		for (int i = 0; i < 0; i ++) {
			initialPos[i] = transform.GetChild (i).position;
		}
	}
	void Update () {
		if (!Camera.main.GetComponent<MainMenu> ().inLoad) {
			if (up) {
				rb.MovePosition (rb.position + Vector2.up * 0.05f);
				distTravelled += 0.05f;
				if (distTravelled >= distance) {
					up = false;
					distTravelled = 0;
				}
			}
			if (!up) {
				rb.MovePosition (rb.position - Vector2.up * 0.05f);
				distTravelled += 0.05f;
				if (distTravelled >= distance) {
				up = true;
					distTravelled = 0;
				}
			}
		}
	}
}
