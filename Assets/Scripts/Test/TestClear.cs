using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClear : MonoBehaviour
{
	public TestGameManager gameManager;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(gameManager.Clear());
		}
	}
}
