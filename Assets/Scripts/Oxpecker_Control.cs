using UnityEngine;
using System.Collections;

public class Oxpecker_Control : MonoBehaviour {
	int attackState = 0;
	float animationStartTime;
	Vector3 startPos;
	public GameObject parent;
	Vector3 lastFrame;
	Vector3 currentFrame;
	Vector3 rhinoDifference;
	public int Strength = 5; //get stronk
	
	bool hasHit = false;
	void Awake () {
		currentFrame = parent.transform.position;
	}
	void Update () {
		lastFrame = currentFrame;
		currentFrame = parent.transform.position;
		if (attackState == 0) {
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				startAnimation (1);
			} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				startAnimation (2);
			} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				startAnimation (3);
			}
			transform.position = parent.transform.position + Vector3.up * 0.4f;
			transform.rotation = parent.transform.rotation;
			hasHit = false;
		} else {
			switch (attackState) {
				case 1:
				Up ();
				break;

				case 2:
				Right ();
				break;

				case 3:
				Left ();
				break;
			}
		}

	}
	void startAnimation (int state) {
		attackState = state;
		animationStartTime = Time.time;
		startPos = parent.transform.position + Vector3.up * 0.4f;
		rhinoDifference = Vector3.zero;
	}
	void Up () {
		transform.rotation = Quaternion.Euler (0,90,0);
		transform.position = Vector3.Lerp (startPos + rhinoDifference,startPos + Vector3.up * 4 + rhinoDifference,(Time.time - animationStartTime) * 5);
		rhinoDifference += currentFrame - lastFrame;
		if ((Time.time - animationStartTime) * 7 > 1 || hasHit) {
			transform.position = startPos + rhinoDifference;
			attackState = 0;
		}
	}
	void Right () {
		transform.rotation = Quaternion.Euler (0,0,0);
		transform.position = Vector3.Lerp (startPos + rhinoDifference,startPos + Vector3.right * 4 + rhinoDifference,(Time.time - animationStartTime) * 5);
		rhinoDifference += currentFrame - lastFrame;
		if ((Time.time - animationStartTime) * 7 > 2 || hasHit) {
			transform.position = startPos + rhinoDifference;
			attackState = 0;
		}
	}
	void Left () {
		transform.rotation = Quaternion.Euler (0,180,0);
		transform.position = Vector3.Lerp (startPos + rhinoDifference,startPos + Vector3.left * 4 + rhinoDifference,(Time.time - animationStartTime) * 5);
		rhinoDifference += currentFrame - lastFrame;
		if ((Time.time - animationStartTime) * 7 > 2 || hasHit) {
			transform.position = startPos + rhinoDifference;
			attackState = 0;
		}
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "EnemyHitbox" && !hasHit && attackState != 0) {
			hasHit = true;
			other.gameObject.transform.parent.gameObject.SendMessage ("Knockback", Strength * 10 * (transform.position - parent.transform.position), SendMessageOptions.DontRequireReceiver);
			other.gameObject.transform.parent.gameObject.SendMessage ("TakeDamage", Strength, SendMessageOptions.DontRequireReceiver);
		}
	}
}
