using System;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour {
	private EnemySpawnPoint[] spawnPoints;

	private void Start() {
		spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
	}
}