using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClearButton : MonoBehaviour
{
	private GameObject clear;

	private void Awake()
	{
		clear = FindObjectOfType<TestClear>().gameObject;
	}

	private void Start()
	{
		clear.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !clear.activeSelf)
		{
			clear.SetActive(true);
		}
	}
}
