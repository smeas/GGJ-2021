using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(10)]
public class PlayerUIScore : MonoBehaviour {
	public GameObject UIScorePrefab;

	private TextMeshProUGUI[] texts;

	private void Start() {
		Setup();

		GameManager.Instance.onPointAdded.AddListener(HandlePointAdd);
	}

	private void Setup() {
		ToyData[] toys = ToyManager.Instance.toys;
		texts = new TextMeshProUGUI[toys.Length];

		GameObject go;
		for (int i = 0; i < toys.Length; i++) {
			go = Instantiate(UIScorePrefab, transform);

			go.GetComponentInChildren<Image>().sprite = toys[i].sprite;
			texts[i] = go.GetComponentsInChildren<TextMeshProUGUI>()[1];
			texts[i].text = GameManager.Instance.toyCounts[i].ToString();
		}
	}

	private void HandlePointAdd() {
		for (int i = 0; i < texts.Length; i++)
			texts[i].text = GameManager.Instance.toyCounts[i].ToString();
	}
}
