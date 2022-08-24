using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRazer : MonoBehaviour
{
	public GameObject razer;

	public float delay;

	private float countTime1 = 0;

	private float countTime2 = 0;

	private void Update()
	{
		countTime1 += Time.deltaTime;

		if (countTime1 < delay) return;

		razer.SetActive(true);

		countTime2 += Time.deltaTime;

		if (countTime2 < 1) return;

		razer.SetActive(false);
		countTime1 = 0;
		countTime2 = 0;
	}
}
