using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text leftArrow;
    public Text rightArrow;

    public Text space;

    public PlayerMovement player;


    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        leftArrow.text = "x" + TextRendering(KeyCode.LeftArrow).ToString();
        rightArrow.text = "x" + TextRendering(KeyCode.RightArrow).ToString();
        space.text = "x" + TextRendering(KeyCode.Space).ToString();
    }

    public int TextRendering(KeyCode key)
    {
        int time = 0;

        for(int i = 0; i < player.inputkeylimit.Count; i++)
        {
            if(player.inputkeylimit[i].key == key)
            {
                time = player.inputkeylimit[i].times;
            }   
            
        }

        return time;
    }
}
