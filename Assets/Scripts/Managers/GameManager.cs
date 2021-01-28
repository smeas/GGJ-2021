using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {
	public int score;

	// TODO: Replace with real UI.
	private void OnGUI() {
		GUILayout.Label($"Score: {score}");
	}
}