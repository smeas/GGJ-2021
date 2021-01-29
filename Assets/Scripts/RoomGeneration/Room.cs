using System;
using UnityEngine;

public class Room : MonoBehaviour {
	public LoadingZoneHandler loadingZoneHandler;
	public BorderHandler borderHandler;
	public EnemySpawnHandler enemySpawnHandler;

	public void Initialize(Direction direction) {
		borderHandler.OpenExits();
		loadingZoneHandler.ActivateLoadingZones(direction);
		enemySpawnHandler.InitializeEnemySpawning();
	}
}
