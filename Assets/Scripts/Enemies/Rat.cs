using System;
using UnityEngine;

public class Rat : MonoBehaviour {
	public Transform backpack;

	[NonSerialized] public Toy toy;

	private void Start() {
		toy = ToyManager.Instance.CreateRandom();
		toy.transform.SetParent(backpack, false);
	}
}