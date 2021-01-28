using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEventHandling : MonoBehaviour {
	public Health health;
	public SceneReference MenuScene;

	private void Start() {
		health.onDeath.AddListener(HandleDeath);
	}

	private void HandleDeath() {
		SceneManager.LoadScene(MenuScene);
	}
}
