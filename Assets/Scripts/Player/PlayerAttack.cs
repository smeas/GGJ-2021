using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public Transform hand;
	public GameObject weapon;
	public float attackDuration = 1f;

	private Camera mainCamera;
	private PlayerController playerController;
	private bool isAttacking;

	private void Start() {
		mainCamera = Camera.main;
		playerController = GetComponent<PlayerController>();
		weapon.SetActive(false);
	}

	private void Update() {
		if (!isAttacking && Input.GetButton("Fire1")) {
			isAttacking = true;

			Vector3 mouseDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
			hand.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			StartCoroutine(CoAttack());
		}
	}

	private IEnumerator CoAttack() {
		weapon.SetActive(true);
		yield return new WaitForSeconds(attackDuration);
		weapon.SetActive(false);
		isAttacking = false;
	}
}