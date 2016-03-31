using UnityEngine;
using System.Collections;

public class Scrolling : MonoBehaviour {
	void Update () {
		transform.position += Vector3.left * Time.deltaTime * 10;
	}
}
