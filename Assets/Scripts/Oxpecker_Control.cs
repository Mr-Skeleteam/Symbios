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
		transform.position = Vector3.Lerp (startPos + rhinoDifference,startPos + Vector3.up * 2 + rhinoDifference,(Time.time - animationStartTime) * 5);
		rhinoDifference += currentFrame - lastFrame;
		if ((Time.time - animationStartTime) * 10 > 1) {
			transform.position = startPos + rhinoDifference;
			attackState = 0;
		}
	}
	void Right () {
		transform.rotation = Quaternion.Euler (0,0,0);
		transform.position = Vector3.Lerp (startPos + rhinoDifference,startPos + Vector3.right * 2 + rhinoDifference,(Time.time - animationStartTime) * 2.5f);
		rhinoDifference += currentFrame - lastFrame;
		if ((Time.time - animationStartTime) * 10 > 1) {
			transform.position = startPos + rhinoDifference;
			attackState = 0;
		}
	}
	void Left () {
		transform.rotation = Quaternion.Euler (0,180,0);
		transform.position = Vector3.Lerp (startPos + rhinoDifference,startPos + Vector3.left * 2 + rhinoDifference,(Time.time - animationStartTime) * 2.5f);
		rhinoDifference += currentFrame - lastFrame;
		if ((Time.time - animationStartTime) * 10 > 1) {
			transform.position = startPos + rhinoDifference;
			attackState = 0;
		}
	}
}
