using UnityEngine;

public class DamageArea : MonoBehaviour {
	private void OnCollisionEnter2D(Collision2D other) {
		Health health = other.gameObject.GetComponent<Health>();
		if (health != null) {
			health.Damage(1);
		}
	}
}
