using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{

    public bool isHit;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    private void OnEnable()
    {
        isHit = false;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }


    void Update()
    {
        if (playerMovement.input.x == 1)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("IsMove", true);
        }
        else if (playerMovement.input.x == -1)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);
        }


        animator.SetBool("OnAir", !playerMovement.isGround);
        animator.SetFloat("Yvelocity", playerMovement.rigid.velocity.y);



    }

    public void DeadAnimation()
    {
        if (isHit)
        {

        }
        else
        {
            animator.SetTrigger("IsDead");
            isHit = true;
        }
    }
}
