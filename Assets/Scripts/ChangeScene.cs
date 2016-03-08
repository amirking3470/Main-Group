using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene: MonoBehaviour {
	[SerializeField] private string levelName;
	// changes to Level scene
	public void LoadLevel () {
		SceneManager.LoadScene(levelName);
	}
}
