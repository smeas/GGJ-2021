using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour {
	public Room startRoom;
	public Room[] roomPrefabs;

	private SnappingCamera snappingCamera;

	private Room currentRoom;
	private Room nextRoom;
	private Vector2 nextGridCell;

	private Transform playerTransform;

	private void Start() {
		snappingCamera = Camera.main.GetComponent<SnappingCamera>();
		currentRoom = startRoom;
		playerTransform = GameObject.FindWithTag("Player").transform;
	}

	// Update keep track player grid position
	// Collider on the whole(middle of) room TODO: WORK ON THIS
	// Triggers act as both enter and loading zones

	private void Update() {
		Vector2 playerGridCell = MathX.Floor(snappingCamera.WorldToGrid(playerTransform.position));
		Vector2 currentRoomGridCell = MathX.Floor(snappingCamera.WorldToGrid(currentRoom.transform.position));

		if (playerGridCell != currentRoomGridCell)
			HandleEnteringNewRoom(playerGridCell - currentRoomGridCell);
	}

	private void HandleEnteringNewRoom(Vector2 direction) {
		Destroy(currentRoom.gameObject);

		currentRoom = nextRoom;
		nextRoom = null;
	}

	private Room GetRandomRoom() {
		int index = Random.Range(0, roomPrefabs.Length - 1);

		Room room = roomPrefabs[index];
		Swap(ref roomPrefabs[index], ref roomPrefabs[roomPrefabs.Length - 1]);
		return room;
	}

	public void LoadRoom(Direction direction) {
		if (nextRoom != null) {
			MoveNextRoom(direction);
			return;
		}

		Vector2 nextGridPosition = snappingCamera.WorldToGrid(currentRoom.transform.position) + direction.ToVector();
		nextGridCell = MathX.Floor(nextGridPosition);

		nextRoom = Instantiate(GetRandomRoom(), snappingCamera.GridToWorld(nextGridPosition), Quaternion.identity);
		nextRoom.loadingZoneHandler.SetEnterDirection(direction);
	}

	private void MoveNextRoom(Direction direction) {
		Vector2 nextGridPosition = snappingCamera.WorldToGrid(currentRoom.transform.position) + direction.ToVector();

		nextRoom.transform.position = snappingCamera.GridToWorld(nextGridPosition);
		nextRoom.loadingZoneHandler.SetEnterDirection(direction);
	}

	private static void Swap<T>(ref T a, ref T b) {
		T temp = a;
		a = b;
		b = temp;
	}
}
