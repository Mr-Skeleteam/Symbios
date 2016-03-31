using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject Rhino;
	public GameObject Oxpecker;
	public GameObject Fade;
	public bool inLoad = false;
	public bool inMainMenu = true;
	// Use this for initialization
	void Start () {
		Fade.GetComponent<SpriteRenderer> ().color = new Color (0,0,0,0);
		Object.DontDestroyOnLoad (gameObject);
	}
	public void StartGame () {
		StartCoroutine (FadeToBlack (1));
		inLoad = true;
		inMainMenu = false;
	}

	public void LoadScene (int SceneID) {
		inLoad = true;
		StartCoroutine (FadeToBlack (SceneID));
	}

	public void ReloadScene () {
		inLoad = true;
		int index = SceneManager.GetActiveScene ().buildIndex;
        StartCoroutine (FadeToBlack (SceneManager.GetActiveScene ().buildIndex));
	}

	IEnumerator FadeToBlack (int SceneID) {
		for (int i = 0; i < 256; i+=2) {
			Fade.GetComponent<SpriteRenderer> ().color = new Color (0,0,0,(float)(i/256f));
			yield return new WaitForSeconds (.01f);
		}
		SceneManager.LoadScene (SceneID);
		transform.position = Vector3.zero;
		StartCoroutine ("FadeFromBlack");
	}
	IEnumerator FadeFromBlack () {
		yield return new WaitForSeconds (0.4f);
		for (int i = 0; i < 256; i+=2) {
			Fade.GetComponent<SpriteRenderer> ().color = new Color (0,0,0,1-(float) (i / 256f));
			yield return new WaitForSeconds (.01f);
		}
		inLoad = false;
	}
}
