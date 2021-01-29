using System;
using DG.Tweening;
using UnityEngine;

public class Rat : MonoBehaviour {
	private static readonly int speedYHash = Animator.StringToHash("speedY");
	private static readonly int speedXHash = Animator.StringToHash("speedX");

	public Transform backpack;
	public GameObject weapon;
	public Collider2D navigationCollider;
	public float deathFadeDuration = 1f;

	private Toy toy;
	private Animator animator;
	private Rigidbody2D rb2d;

	private void Start() {
		toy = ToyManager.Instance.CreateRandom();
		toy.transform.SetParent(backpack, false);

		GetComponent<Health>().onDeath.AddListener(OnDeath);
		animator = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		Vector3 localScale = transform.localScale;
		if (rb2d.velocity.x > 0)
			localScale.x = -1;
		else if (rb2d.velocity.x < 0)
			localScale.x = 1;
		transform.localScale = localScale;

		animator.SetFloat(speedYHash, rb2d.velocity.y);
		animator.SetFloat(speedXHash, rb2d.velocity.x);
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
