using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
	public Vector2 path;

	public float speed;

	private void Update()
	{
		transform.Translate(path * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player") && !collision.CompareTag("Enemy"))
		{
			Destroy(gameObject);
		}
	}
}
