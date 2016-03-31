using UnityEngine;
using System.Collections;

public class NextLevelPlayer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "LevelGoal") {
			other.SendMessage ("SwitchLevel");
		}
	}
}
