using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReloadCount : MonoBehaviour
{
	private bool wear = false;

	private void Update()
	{
		if (!wear) return;

		if (Input.GetKeyDown(KeyCode.E))
		{
			TestCountGet[] tests = FindObjectsOfType<TestCountGet>();
			foreach (TestCountGet temporary in tests)
			{
				if (Vector2.Distance(transform.parent.position, temporary.transform.position) < 1.5f)
				{
					if (temporary.isSteal)
					{
						FindObjectOfType<TestKeyLimit>().InputKey(temporary.getKey);
					}
					Destroy(temporary);
				}
			}
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			GetComponent<SpriteRenderer>().enabled = false;
			transform.position = collision.transform.position;
			transform.parent = collision.transform;
			wear = true;
			transform.GetChild(0).gameObject.SetActive(true);
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
