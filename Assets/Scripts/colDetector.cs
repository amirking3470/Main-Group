using UnityEngine;
using System.Collections;

public class colDetector : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;
	public Vector2 colGridPosition = Vector2.zero;
	public Vector2 gridMath = Vector2.zero;
	public bool collide = false;
	public bool dontDelete = false;
	private float timer;

	// Use this for initialization
	void Start () {
		timer = 0;
	}
		
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "badTile") {
			//Debug.Log ("collision at " + gridPosition);
			Debug.Log ("col grid pos " + colGridPosition);
			dontDelete = true;
			collide = true;
		}
		if (collide == true) {
			if (col.gameObject.tag == "colChecker") {
				col.gameObject.GetComponent<colDetector> ().dontDelete = true;
			}
		}
	}

	void deleter () {
		if (dontDelete == false) {
			this.Destroy (this.gameObject);
		}
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer >= 0.1) {
			deleter ();
		}
	}
}
