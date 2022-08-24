using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRush : MonoBehaviour
{
	private bool searching = true;

	private Vector2 target;

	public float speed;

	public float waitTime;

	private void Start()
	{
		target = transform.position;
	}

	private void Update()
	{
		if (searching) return;

		waitTime -= Time.deltaTime;

		if (waitTime > 0) return;
		
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

		GetComponent<SpriteRenderer>().flipX = target.x - transform.position.x < 0;

		if (Vector3.Distance(transform.position, target) < 0.1f)
		{
			GetComponent<TestRush>().enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && searching)
		{
			target = collision.transform.position;
			searching = false;
		}
	}
}
