using DG.Tweening;
using UnityEngine;

public class Rat : MonoBehaviour {
	private static readonly int speedYRatioHash = Animator.StringToHash("speedYRatio");
	private static readonly int speedXRatioHash = Animator.StringToHash("speedXRatio");

	public Transform backpack;
	public GameObject weapon;
	public Collider2D navigationCollider;
	public float deathFadeDuration = 1f;
	private Animator animator;
	private RatAI ratAI;
	private Rigidbody2D rb2d;

	private Toy toy;

	private void Start() {
		toy = ToyManager.Instance.CreateRandom();
		toy.transform.SetParent(backpack, false);
		toy.GetComponent<DepthSort>().positionSource = transform;

		GetComponent<Health>().onDeath.AddListener(OnDeath);
		animator = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		ratAI = GetComponent<RatAI>();
	}

	private void Update() {
		Vector3 localScale = transform.localScale;
		if (rb2d.velocity.x > 0)
			localScale.x = -1;
		else if (rb2d.velocity.x < 0)
			localScale.x = 1;
		transform.localScale = localScale;

		animator.SetFloat(speedYRatioHash, rb2d.velocity.y / ratAI.maxVelocity);
		animator.SetFloat(speedXRatioHash, rb2d.velocity.x / ratAI.maxVelocity);
	}

	private void OnDeath() {
		toy.Drop(transform);

		weapon.SetActive(false);
		navigationCollider.enabled = false;
		GetComponent<Collider2D>().enabled = false;
		GetComponent<RatAI>().enabled = false;
		GetComponent<Rigidbody2D>().velocity = default;

		GetComponent<SpriteRenderer>()
			.DOFade(0, deathFadeDuration)
			.OnComplete(() => Destroy(gameObject));
	}
}
