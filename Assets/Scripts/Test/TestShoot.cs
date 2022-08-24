using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShoot : MonoBehaviour
{
	public GameObject bullet;

	public Vector2 shootPath;

	public float delay;

	private float countTime;

	private void Update()
	{
		countTime += Time.deltaTime;

		if (countTime < delay) return;
		
		countTime = 0;

		GameObject game = Instantiate(bullet, transform.position + new Vector3(0, 0.1f), Quaternion.identity);
		game.GetComponent<TestBullet>().path = shootPath;
	}
}