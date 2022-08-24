using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCountGet : MonoBehaviour
{
	public KeyCode getKey;

	public bool isSteal = false;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player") && !isSteal)
		{
			FindObjectOfType<TestKeyLimit>().DiscountKey(getKey);
			isSteal = true;
		}
	}
}
