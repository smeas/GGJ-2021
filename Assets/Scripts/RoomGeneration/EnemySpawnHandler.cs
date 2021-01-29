using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnHandler : MonoBehaviour {
	public Rat enemyPrefab;
	public int maxEnemies = 5;
	public float spawnInterval = 2f;

	private EnemySpawnPoint[] spawnPoints;
	private Rat[] spawnedEnemies;

	private void Start() {
		spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
		spawnedEnemies = new Rat[maxEnemies];
	}

	public void InitializeEnemySpawning() {
		for (int i = 0; i < spawnedEnemies.Length - 1; i++) {
			if (spawnedEnemies[i] != null) continue;

			SpawnEnemy(i);
		}

		StartCoroutine(CoContinousSpawning());
	}

	private void SpawnEnemy(int index) {
		EnemySpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Rat newRat = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);

		Health ratHealth = newRat.GetComponent<Health>();
		ratHealth.onDeath.AddListener(() => HandleEnemyDeath(index));

		spawnedEnemies[index] = newRat;
		spawnedEnemies[index].transform.parent = transform;
	}

	private void HandleEnemyDeath(int index) {
		spawnedEnemies[index] = null;
	}

	private IEnumerator CoContinousSpawning() {
		float interval = spawnInterval;
		yield return new WaitForSeconds(interval);

		while (true) {
			for (int i = 0; i < spawnedEnemies.Length - 1; i++) {
				if (spawnedEnemies[i] == null) {
					SpawnEnemy(i);
					break;
				}
			}

			yield return new WaitForSeconds(interval);
		}
	}
}
