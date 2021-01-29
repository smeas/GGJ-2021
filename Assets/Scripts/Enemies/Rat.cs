using System;
using DG.Tweening;
using UnityEngine;

public class Rat : MonoBehaviour {
	public Transform backpack;
	public GameObject weapon;
	public Collider2D navigationCollider;
	public float deathFadeDuration = 1f;

	private Toy toy;

	private void Start() {
		toy = ToyManager.Instance.CreateRandom();
		toy.transform.SetParent(backpack, false);

		GetComponent<Health>().onDeath.AddListener(OnDeath);
	}

	private void OnDeath() {
		toy.Drop(transform);

		weapon.SetActive(false);
		navigationCollider.enabled = false;
		GetComponent<Collider2D>().enabled = false;
		GetComponent<RatAI>().enabled = false;

		GetComponent<SpriteRenderer>()
			.DOFade(0, deathFadeDuration)
			.OnComplete(() => Destroy(gameObject));
	}
}