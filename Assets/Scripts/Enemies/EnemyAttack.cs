using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	public Transform hand;
	public GameObject weapon;
	public float attackDuration = 1f;
	public float attackCooldown = 1f;

	private bool isAttacking;
	private float lastAttackStartTime;

	private void Start() {
		weapon.SetActive(false);
	}

	public void Attack(Vector2 direction) {
		if (isAttacking || Time.time - lastAttackStartTime < attackDuration + attackCooldown) return;
		isAttacking = true;
		lastAttackStartTime = Time.time;

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		hand.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		StartCoroutine(CoAttack());
	}

	private IEnumerator CoAttack() {
		weapon.SetActive(true);
		yield return new WaitForSeconds(attackDuration);
		weapon.SetActive(false);
		isAttacking = false;
	}
}