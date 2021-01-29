using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;

	public Vector2 lastNonZeroMovement;

	private Rigidbody2D rb2d;
	private Animator animator;
	private PlayerAttack playerAttack;
	private Vector2 direction;

	private Vector2 movement;

	private void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerAttack = GetComponent<PlayerAttack>();
	}

	private void Update() {
		movement = new Vector2(Input.GetAxisRaw("Horizontal"),
		                       Input.GetAxisRaw("Vertical"));

		if (movement.x != 0 || movement.y != 0)
			lastNonZeroMovement = movement;

		if (!playerAttack.isAttacking) {
			FlipObject(movement);
			HandleMovementSprite(movement);
		}
	}

		private void HandleMovementSprite(Vector2 currDirection) {
		if (currDirection.y == 0) {
			if (currDirection.x != 0)
				animator.Play("Side");
		}
		else {
			animator.Play(lastNonZeroMovement.y > 0 ? "Up" : "Down");
		}
	}

	public void FlipObject(Vector2 newDirection) {
		Vector3 localScale = transform.localScale;
		if (newDirection.x > 0 || (newDirection.y != 0))
			localScale.x = 1;
		else if (newDirection.x < 0)
			localScale.x = -1;
		transform.localScale = localScale;

		direction = newDirection;
	}

	private void FixedUpdate() {
		Vector2 delta = movement.normalized * (speed * Time.deltaTime);
		rb2d.MovePosition(transform.position + (Vector3)delta);
	}
}
