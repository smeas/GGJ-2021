using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
	public int maxHealth = 3;
	public int currentHealth = 3;

	public UnityEvent onDeath;

	private void Start() {
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}

	public void Damage(int incomingDamage) {
		currentHealth -= incomingDamage;

		if (currentHealth <= 0)
			HandleWakeUp();
	}

	private void HandleWakeUp() {
		onDeath.Invoke();
	}
}
