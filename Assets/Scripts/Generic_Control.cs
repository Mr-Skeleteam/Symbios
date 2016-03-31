using UnityEngine;
using System.Collections;

public class Generic_Control : MonoBehaviour {
	public bool isEnabled = true;
	Rigidbody2D rb;
	Quaternion goal;
	public int health;
	public int maxHealth;
	public bool invincible = false;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		goal = Quaternion.Euler (0,0,0);
		health = maxHealth;
	}

	void FixedUpdate () {
		if (!Camera.main.GetComponent<MainMenu> ().inLoad) {
			int direction = 0; // -1 = left; 1 = right
			if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.01f) {
				direction = (int) (Input.GetAxis ("Horizontal") / (Mathf.Abs (Input.GetAxis ("Horizontal"))));
				goal = Quaternion.Euler (0,90 - 90 * direction,0);
			}
			transform.rotation = Quaternion.Slerp (transform.rotation,goal,0.3f);
		}
	}
	
	public void OnGUI () {
		if (!Camera.main.GetComponent<MainMenu> ().inLoad) {
			Texture2D Red = new Texture2D (1,1);
			Red.SetPixel (1,1,Color.red);
			Red.Apply ();
			Texture2D Green = new Texture2D (1,1);
			Green.SetPixel (1,1,Color.green);
			Green.Apply ();
			Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
			Vector3 screenPos = new Vector2 (pos.x,Camera.main.pixelHeight - pos.y);
			GUI.DrawTexture (new Rect (screenPos + new Vector3 (-40,-100),new Vector2 (80,10)),Red);
			GUI.DrawTexture (new Rect (screenPos + new Vector3 (-40,-100),new Vector2 (80 * health / maxHealth,10)),Green);
		}
	}

	public void TakeDamage (int Damage) {
		if (health > 0 && !invincible) {
			health -= Damage;
			StartCoroutine ("Invincibility",60);
		}
	}

	IEnumerator Invincibility (int frames) {
		invincible = true;
		for (int i = 0; i < frames; i++) {
			yield return new WaitForEndOfFrame ();
			GetComponent<SpriteRenderer> ().enabled = i % 8 < 4;
		}
		GetComponent<SpriteRenderer> ().enabled = true;
		invincible = false;
	}
}
