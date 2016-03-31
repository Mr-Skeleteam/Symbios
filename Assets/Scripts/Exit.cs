using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !Camera.main.GetComponent<MainMenu> ().inLoad) {
			Application.Quit ();
		}
	}
}
