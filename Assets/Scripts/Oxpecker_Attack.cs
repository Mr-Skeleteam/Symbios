using UnityEngine;
using System.Collections;

public class Oxpecker_Attack : MonoBehaviour {
	Animator anim;
	bool isAttacking;
	const int IDLE = 0;
    const int UP = 1;
    const int RIGHT = 2;
	const int LEFT = 3;
	int currentState = IDLE;
	void Awake () {
		anim = GetComponent<Animator> ();
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) && currentState == 0) {
			changeState (1);
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && currentState == 0) {
			changeState (2);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && currentState == 0) {
			changeState (3);
		}
	}
	void changeState (int state) {
		if (currentState == state) return;
		switch (state) {
			case IDLE:
			anim.SetInteger ("State",IDLE);
			break;

			case UP:
			anim.SetInteger ("State",UP);
			break;

			case RIGHT:
			anim.SetInteger ("State",RIGHT);
			break;

			case LEFT:
			anim.SetInteger ("State",LEFT);
			break;
        }
		currentState = state;
	}
}
