using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
	public float speed;

	public bool doFlip;

	public Transform[] path = new Transform[2];

	private int count = 0;

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, path[count].position, speed * Time.deltaTime);

		if (doFlip)
		{
			GetComponent<SpriteRenderer>().flipX = path[count].position.x - transform.position.x > 0;
		}

		if (Vector3.Distance(transform.position, path[count].position) < 0.1f)
		{
			count = count == 1 ? 0 : 1;
		}
	}
}
