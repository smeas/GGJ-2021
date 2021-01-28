using System;
using DG.Tweening;
using UnityEngine;

public class Rat : MonoBehaviour {
	public Transform backpack;
	public float deathFadeDuration = 1f;

	private Toy toy;

	private void Start() {
		toy = ToyManager.Instance.CreateRandom();
		toy.transform.SetParent(backpack, false);

		GetComponent<Health>().onDeath.AddListener(OnDeath);
	}

	private void OnDeath() {
		toy.Drop();
		GetComponent<SpriteRenderer>()
			.DOFade(0, deathFadeDuration)
			.OnComplete(() => Destroy(gameObject));
	}
}