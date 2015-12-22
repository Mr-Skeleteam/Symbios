using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamSwitch : MonoBehaviour {
	public List<GameObject> characters = new List<GameObject> (2);
	int activeCharacter = 0;
	Oxpecker_Control oxc;
    void LateUpdate () {
		if (Input.GetKeyUp (KeyCode.Space) && characters[activeCharacter].GetComponent<Generic_Control> ().canSwitch ()) {
			if ((oxc = characters[activeCharacter].GetComponent<Oxpecker_Control> ()) != null) oxc.onSwitchAway ();
			activeCharacter = 1 - activeCharacter;
		}
		characters[activeCharacter].GetComponent<Generic_Control> ().isEnabled = true;
		characters[1 - activeCharacter].GetComponent<Generic_Control> ().isEnabled = false;
		Vector3 goal = characters[activeCharacter].transform.position + Vector3.forward * -10;
		transform.position = Vector3.Lerp (transform.position,goal,0.05f);
    }
}
