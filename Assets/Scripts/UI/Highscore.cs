using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Highscore : MonoBehaviour {
	public TextMeshProUGUI scoreOne;
	public TextMeshProUGUI scoreTwo;
	public TextMeshProUGUI scoreThree;

	public SceneReference menuScene;

	private string[] scores;

	private void Start() {
		scores = PlayerPrefs.GetString("highscore", "0-0-0").Split('-');

		UpdateTextValues();
	}

	private void UpdateTextValues() {
		scoreOne.text = scores[0];
		scoreTwo.text = scores[1];
		scoreThree.text = scores[2];
	}

	public void ReturnToMenu() {
		SceneManager.LoadScene(menuScene);
	}
}
