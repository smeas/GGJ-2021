using System;
using UnityEngine;

public class RatAI : MonoBehaviour {
	public Transform followTarget;
	public float slowRadius = 4f;
	public float stopRadius = 1f;
	[Space]
	public float maxVelocity = 4f;
	public float visionDistance = 5f;
	public float maxSeekForce = 0.5f;
	public float maxAvoidForce = 1f;

	[Space]
	public bool seeking = true;
	public bool collisionAvoidance = true;
	public bool allowMove = true;

	private Rigidbody2D rb2d;
	private int obstacleLayer;
	private int enemyLayer;
	private Vector2 velocity;

	private void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		obstacleLayer = 1 << LayerMask.NameToLayer("Obstacle");
		enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

		if (followTarget == null)
			followTarget = GameObject.FindWithTag("Player").transform;
	}

	private void FixedUpdate() {
		Vector2 steering = Vector2.zero;

		if (seeking)
			steering += Seek(followTarget.position);

		if (collisionAvoidance)
			steering += AvoidObstacles();

		velocity = Vector2.ClampMagnitude(velocity + steering, maxVelocity);
		if (allowMove)
			rb2d.velocity = velocity;
	}

	private Vector2 Seek(Vector2 target) {
		Vector2 direction = target - (Vector2)transform.position;
		float distance = direction.magnitude;

		Vector2 desiredVelocity = Vector2.zero;

		if (distance >= stopRadius) {
			desiredVelocity = direction.normalized * maxVelocity;

			if (distance < slowRadius)
				desiredVelocity *= distance / slowRadius;

			Debug.DrawRay(transform.position, desiredVelocity, Color.red);
		}

		Vector2 steering = desiredVelocity - velocity;
		Debug.DrawRay(transform.position + (Vector3)velocity, steering, Color.blue);

		return Vector2.ClampMagnitude(steering, maxSeekForce);
	}

	private Vector2 AvoidObstacles() {
		Vector2 self = transform.position;

		RaycastHit2D hit = Physics2D.Raycast(self, velocity, visionDistance, obstacleLayer | enemyLayer);
		if (hit.collider == null) return Vector2.zero;

		Debug.DrawLine(transform.position, hit.point, Color.yellow);

		Vector2 obstacle = hit.collider.bounds.center;
		Vector2 selfToObstacle = obstacle - self;
		Vector2 velocityNormalized = velocity.normalized;

		float p = Vector2.Dot(selfToObstacle, velocityNormalized);

		Vector2 avoidance = (self + velocityNormalized * p) - obstacle;

		Debug.DrawRay(hit.collider.bounds.center, avoidance, Color.magenta);

		return avoidance.normalized * maxAvoidForce;
	}
}