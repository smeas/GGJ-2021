using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {
	[NonSerialized]
	public int[] toyCounts;

	private void Start() {
		toyCounts = new int[ToyManager.Instance.toys.Length];
	}

	public void AddScore(ToyData toyType) {
		int index = Array.IndexOf(ToyManager.Instance.toys, toyType);
		if (index == -1) {
			Debug.LogError($"Failed to add score: Toy not found {toyType}");
			return;
		}

		toyCounts[index] += 1;
	}

	// TODO: Replace with real UI.
	private void OnGUI() {
		string scoreText = string.Join(", ", toyCounts);
		GUILayout.Label($"Score: {scoreText}", new GUIStyle {
			fontSize = 24
		});
	}
}