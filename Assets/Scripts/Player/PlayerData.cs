using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int playerHP;

    public int deadCount;

    public int score;
    public int addScore;

    void Awake()
    {
        var obj = FindObjectsOfType<PlayerData>();

        if(obj.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }

    public void Damage()
    {
        playerHP = Mathf.Clamp(playerHP - 1, 0, 5);
    }

    public void AddHP()
    {
        playerHP = Mathf.Clamp(playerHP + 1, 0, 5);
    }

    public void AddScore(int value)
    {
        addScore += value;
    }

    public void StageStart()
    {
        addScore = score;

    }

    public void StageEnd()
    {
        score = addScore;
    }

    
}
