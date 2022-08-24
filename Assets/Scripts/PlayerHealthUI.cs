using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public List<Image> hearts = new List<Image>();

    public PlayerData playerData;

    private void OnEnable()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    private void Update()
    {
        HPUpdate();
    }

    private void HPUpdate()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < playerData.playerHP; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }
}
