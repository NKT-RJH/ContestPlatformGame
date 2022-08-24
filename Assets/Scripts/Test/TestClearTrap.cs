using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClearTrap : MonoBehaviour
{
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			SpriteRenderer sprite = GetComponent<SpriteRenderer>();
			sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
			GetComponent<BoxCollider2D>().isTrigger = false;
		}
	}
}
