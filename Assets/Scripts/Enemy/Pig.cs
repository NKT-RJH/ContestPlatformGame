using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : KinematicObject
{
    public bool isFlip;

    public Transform minMovePos;
    public Transform maxMovePos;

    public SpriteRenderer sprite;

    protected override void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();

        base.OnEnable();
    }

    private void Update()
    {
        if(isFlip)
        {
            MoveTo(new Vector2(1, rigid.velocity.y));

            if (transform.position.x > maxMovePos.position.x)
            {
                isFlip = false;
                
            }
        }
        else
        {
            MoveTo(new Vector2(-1, rigid.velocity.y));

            if (transform.position.x < minMovePos.position.x)
            {
                isFlip = true;
            }
        }

        sprite.flipX = isFlip;



        
        

        

    }
}
