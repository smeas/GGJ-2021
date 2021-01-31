using DG.Tweening;
using UnityEngine;

public class MenuScreens : MonoBehaviour {
	public RectTransform mainScreen;
	public RectTransform creditsScreen;
	public RectTransform scoreScreen;
	private readonly float duration = 0.8f;

	private RectTransform currentScreen;

	private void Start() {
		currentScreen = mainScreen;
	}

	public void ShowMainScreen() {
		DOTween.Sequence()
			.Append(currentScreen.DOMoveX(-currentScreen.rect.width, duration))
			.Append(mainScreen.DOMoveX(0, duration)).SetEase(Ease.InOutSine)
			.Play();
	}

	public void ShowCredits() {
		DOTween.Sequence()
			.Append(mainScreen.DOMoveX(-mainScreen.rect.width, duration))
			.Append(creditsScreen.DOMoveX(0, duration)).SetEase(Ease.InOutSine)
			.Play();

		currentScreen = creditsScreen;
	}

	public void ShowScores() {
		DOTween.Sequence()
			.Append(mainScreen.DOMoveX(-mainScreen.rect.width, duration))
			.Append(scoreScreen.DOMoveX(0, duration)).SetEase(Ease.InOutSine)
			.Play();

		currentScreen = scoreScreen;
	}
}
