using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestGameManager : MonoBehaviour
{
	public static int HP = 5;
	public static bool dead = false;
	public bool GameOver;

	public int NextSceneID;

	public Transform HPImage;
	public GameObject glass;
	public GameObject gameOverText;
	public GameObject gameClearText;
	public GameObject clearParticle;
	public Text gameStartText;

	private void Awake()
	{
		if (dead)
		{
			HP--;
		}

		if (HP <= 0)
		{
			GameOver = true;
		}
		dead = false;
	}

	private void Start()
	{
		if (GameOver)
		{
			StartCoroutine(GameOverStart());
		}
		else
		{
			StartCoroutine(GameStart());
		}
	}

	private void Update()
	{
		for (int count = 0; count < HPImage.childCount; count++)
		{
			HPImage.GetChild(count).gameObject.SetActive(false);
		}

		for (int count = 0; count < HP; count++)
		{
			HPImage.GetChild(count).gameObject.SetActive(true);
		}
	}

	public IEnumerator Clear()
	{
		GameOver = true;

		gameClearText.SetActive(true);
		clearParticle.SetActive(true);

		yield return new WaitForSeconds(2);

		SceneManager.LoadScene(NextSceneID);
	}

	public IEnumerator Dead(bool skip)
	{
		if (!skip)
		{
			TestPlayer player = FindObjectOfType<TestPlayer>();
			while (!player.isGround)
			{
				yield return null;
			}
		}

		dead = true;

		glass.SetActive(true);

		SpriteRenderer sprite = FindObjectOfType<TestPlayer>().GetComponent<SpriteRenderer>();

		for (int count = 255; count >= 0; count -= 3)
		{
			sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, count / 255f);
			yield return null;
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private IEnumerator GameStart()
	{
		GameOver = true;

		gameStartText.gameObject.SetActive(true);
		gameStartText.text = "Ready...";

		yield return new WaitForSeconds(2);

		gameStartText.color = Color.yellow;
		gameStartText.text = "GO!!";

		yield return new WaitForSeconds(1);

		gameStartText.gameObject.SetActive(false);
		GameOver = false;
	}

	private IEnumerator GameOverStart()
	{
		gameOverText.SetActive(true);
		
		glass.SetActive(true);
		
		Image image = glass.GetComponent<Image>();

		for (int count = (int)image.color.a * 255; count <= 255; count += 4)
		{
			image.color = new Color(image.color.r, image.color.g, image.color.b, count / 255f);
			yield return null;
		}

		HP = 5;

		SceneManager.LoadScene("TestGameOver");
	}
}
