using System.Collections.Generic;
using UnityEngine;

public class TestKeyLimit : MonoBehaviour
{
	public TestGameManager gameManager;

	public List<TestLimit> limit;

	private void Update()
	{
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

		int count = 0;
		foreach (TestLimit temporary in limit)
		{
			if (temporary.count <= 0)
			{
				count++;
			}
		}
		if (count == limit.Count)
		{
			StartCoroutine(gameManager.Dead(false));
		}
	}

	public void DiscountKey(KeyCode key)
	{
		foreach (TestLimit temporary in limit)
		{
			if (temporary.key == key)
			{
				temporary.count--;
			}
		}
	}

	public void InputKey(KeyCode key)
	{
		foreach (TestLimit temporary in limit)
		{
			if (temporary.key == key)
			{
				temporary.count++;
			}
		}
	}

	public int GetCount(KeyCode key)
	{
		foreach (TestLimit temporary in limit)
		{
			if (temporary.key == key)
			{
				return temporary.count;
			}
		}

		return -1;
	}
}

[System.Serializable]
public class TestLimit
{
	public KeyCode key;
	public int count;
}