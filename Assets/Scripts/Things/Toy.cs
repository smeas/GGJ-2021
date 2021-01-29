using System;
using UnityEngine;

public class Toy : MonoBehaviour {
	public ToyData data;

	private Collider2D pickupTrigger;

	private void Start() {
		GetComponent<SpriteRenderer>().sprite = data.sprite;
		pickupTrigger = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.isTrigger && other.attachedRigidbody.CompareTag("Player")) {
			GameManager.Instance.AddScore(data.points);
			Destroy(gameObject);
		}
	}

	public void Drop(Transform root) {
		transform.parent = root.parent;
		pickupTrigger.enabled = true;
	}
}