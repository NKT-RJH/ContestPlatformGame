using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public PlayerData player;

    private void OnEnable()
    {
        scoreText = GetComponent<Text>();
        player = FindObjectOfType<PlayerData>();
    }

    private void Update()
    {
        scoreText.text = "Score : " + player.addScore.ToString();
    }
}
