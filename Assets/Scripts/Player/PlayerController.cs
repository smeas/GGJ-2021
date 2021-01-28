using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;

	public Vector2 lastNonZeroMovement;

	private Rigidbody2D rb2d;
	private Vector2 movement;

	private void Start() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		movement = new Vector2(Input.GetAxisRaw("Horizontal"),
		                       Input.GetAxisRaw("Vertical"));

		if (movement.x != 0 || movement.y != 0)
			lastNonZeroMovement = movement;
	}

	private void FixedUpdate() {
		Vector2 delta = movement.normalized * (speed * Time.deltaTime);
		rb2d.MovePosition(transform.position + (Vector3)delta);
	}
}