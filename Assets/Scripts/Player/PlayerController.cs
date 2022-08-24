using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerMovement movement;
    public PlayerSprite playerSprite;

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerSprite = GetComponent<PlayerSprite>();
        movement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
    
    }

    void Update()
    {
        if (PlayerInputLimitCheck())
        {
			DeadEvent();
        }
        
    }

    public void DeadEvent()
    {
        movement.inputLock = true;
        playerSprite.DeadAnimation();
    }

    public void Dead()
    {
        gameManager.Dead();
    }

    public bool PlayerInputLimitCheck()
    {
        int num = 0;

		foreach (KeyLimit keyLimit in movement.inputkeylimit)
		{
			if (keyLimit.times <= 0)
			{
				num++;
			}
		}

		return num == movement.inputkeylimit.Count;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            DeadEvent();
        }
    }

}
