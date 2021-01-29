using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public Transform hand;
	public SpriteRenderer weapon;
	public Sprite sideSweepSprite;
	public Sprite upDownSweepSprite;
	public float attackDuration = 1f;
	public bool isAttacking;

	private Camera mainCamera;
	private PlayerController playerController;
	private Animator animator;

	private void Start() {
		mainCamera = Camera.main;
		playerController = GetComponent<PlayerController>();
		animator = GetComponent<Animator>();

		weapon.enabled = false;
	}

	private void Update() {
		if (!isAttacking && Input.GetButton("Fire1")) {
			isAttacking = true;

			Vector3 mouseDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

			Direction direction = GetDirectionFromAngle(angle);
			Vector2 dirVec = direction.ToVector();

			playerController.FlipObject(dirVec);

			if (direction == Direction.Left)
				dirVec = direction.Inverted().ToVector();
			else
				dirVec = direction.ToVector();

			angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
			hand.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			StartCoroutine(CoAttack(dirVec));
		}
	}

	private Direction GetDirectionFromAngle(float angle) {
		if (angle > -135 && angle <= -45)
			return Direction.Down;
		if (angle > -45 && angle <= 45)
			return Direction.Right;
		if (angle > 45 && angle <= 135)
			return Direction.Up;

		return Direction.Left;
	}

	private IEnumerator CoAttack(Vector2 direction) {
		weapon.enabled = true;
		HandleAttackAnimation(direction);

		yield return new WaitForSeconds(attackDuration);

		weapon.enabled = false;
		isAttacking = false;
	}

	private void HandleAttackAnimation(Vector2 direction) {
		Vector2 dir = direction;

		if (dir.y == 0) {
			if (dir.x != 0) {
				weapon.sprite = sideSweepSprite;
				animator.Play("Attack - Side");
			}
		}
		else {
			weapon.sprite = upDownSweepSprite;
			animator.Play(dir.y > 0 ? "Attack - Up" : "Attack - Down");
		}
	}
}
