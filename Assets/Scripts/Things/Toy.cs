using UnityEngine;

public class Toy : MonoBehaviour {
	public ToyData data;

	private void Start() {
		GetComponent<SpriteRenderer>().sprite = data.sprite;
	}
}