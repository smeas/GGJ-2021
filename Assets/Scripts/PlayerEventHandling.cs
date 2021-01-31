using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEventHandling : MonoBehaviour {
	public Health health;
	public SceneReference MenuScene;

	public SpriteRenderer spriteRenderer;
	public float flickerAlpha;
	public float flickerDuration = 0.5f;
	public float flickerInterval = 0.1f;

	private bool receivedDamage;

	private void Start() {
		health.onDeath.AddListener(HandleDeath);
		health.onDamageReceived.AddListener(HandleReceivedDamage);
	}

	private void HandleReceivedDamage() {
		if (receivedDamage) return;

		StartCoroutine(CoHandleReceivedDamage());
	}

	private IEnumerator CoHandleReceivedDamage() {
		health.isInvulnerable = true;
		receivedDamage = true;
		bool isFlickerActive = false;
		for (float i = 0; i < flickerDuration; i += flickerInterval) {
			Color col = spriteRenderer.color;
			col.a = isFlickerActive ? 1 : flickerAlpha;
			spriteRenderer.color = col;

			isFlickerActive = !isFlickerActive;
			yield return new WaitForSeconds(flickerInterval);
		}

		Color color = spriteRenderer.color;
		color.a = 1;
		spriteRenderer.color = color;
		health.isInvulnerable = false;
		receivedDamage = false;
	}

	private void HandleDeath() {
		SceneManager.LoadScene(MenuScene);
	}
}
