using UnityEngine;
using System.Collections;

public class LevelGoal : MonoBehaviour {
	public int TargetLevel;
	public Vector3 RhinoStartPos;
	void SwitchLevel () {
		Camera.main.GetComponent<MainMenu> ().LoadScene (TargetLevel);
	}
}
