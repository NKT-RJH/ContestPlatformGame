using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTimeScore : MonoBehaviour
{
	public TestGameManager gameManager;

	public static float time;
	public static int score;

	public Text scoreText;
	public Text timeText;

	private void Update()
	{
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

		time += Time.deltaTime;

		scoreText.text = score + " ¡°";
		timeText.text = (int)(time / 60) + "∫– " + (int)(time % 60) + "√ ";
	}

	public static void AddScore(int score)
	{
		TestTimeScore.score += score;
	}
}
