using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnHandler : MonoBehaviour {
	public Rat enemyPrefab;
	public int maxEnemies = 5;
	public float spawnInterval = 0.2f;
	public float clearSpawnArea = 1;

	private EnemySpawnPoint[] spawnPoints;

	private Transform playerTransform;

	private void Start() {
		playerTransform = GameObject.FindWithTag("Player").transform;

		spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
	}

	public void InitializeEnemySpawning() {
		StartCoroutine(CoContinousSpawning());
	}

	private void SpawnEnemy() {
		EnemySpawnPoint spawnPoint = GetSpawnPoint();
		Rat newRat = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
		newRat.transform.parent = transform;
	}

	private EnemySpawnPoint GetSpawnPoint() {
		EnemySpawnPoint spawnPoint;
		do {
			spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
		} while (IsCloseToPlayer(spawnPoint));

		return spawnPoint;
	}

	private bool IsCloseToPlayer(EnemySpawnPoint spawnPoint) {
		float sqrDistance = (playerTransform.position - spawnPoint.transform.position).sqrMagnitude;
		return sqrDistance <= clearSpawnArea * clearSpawnArea;
	}

	private IEnumerator CoContinousSpawning() {
		float interval = spawnInterval;

		for (int i = 0; i < maxEnemies; i++) {
			SpawnEnemy();
			yield return new WaitForSeconds(interval);
		}
	}
}
