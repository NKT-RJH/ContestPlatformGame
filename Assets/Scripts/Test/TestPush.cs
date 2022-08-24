using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPush : MonoBehaviour
{
	public Transform push;

	public Transform target;

	public float speed;

	public float delay;

	private float countTime;

	private Vector2 originPath;

	private Coroutine coroutine = null;

	private void Start()
	{
		originPath = push.position;
	}

	private void Update()
	{
		if (coroutine != null) return;
	
		countTime += Time.deltaTime;

		if (countTime < delay) return;

		countTime = 0;

		coroutine = StartCoroutine(PushStart());
	}

	private IEnumerator PushStart()
	{
		while (Vector2.Distance(push.position, target.position) > 0.1f)
		{
			push.position = Vector2.MoveTowards(push.position, target.position, speed * Time.deltaTime);
			yield return null;
		}

		yield return new WaitForSeconds(0.2f);

		while (Vector2.Distance(push.position, originPath) > 0.1f)
		{
			push.position = Vector2.MoveTowards(push.position, originPath, speed * Time.deltaTime / 2);
			yield return null;
		}

		coroutine = null;
	}
}
