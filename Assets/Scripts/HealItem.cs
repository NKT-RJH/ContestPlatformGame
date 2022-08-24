using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public PlayerData playerData;

    private void OnEnable()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerData.playerHP < 5)
        {
            playerData.AddHP();
            Destroy(this.gameObject);
        }
    }
}
