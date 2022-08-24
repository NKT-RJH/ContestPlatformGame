using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : KinematicObject
{
    public bool isCatch;
    public bool isRush;

    public SpriteRenderer sprite;
    public Animator animator;
    public PlayerMovement playerMovement;

    public Vector2 targetpos;

    protected override void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        isRush = false;
        isCatch = false;

        targetpos = new Vector2(0, 0);
        base.OnEnable();
    }

    private void Update()
    {
        if (isCatch && !isRush)
        {
            transform.position += new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y, transform.position.z).normalized * nowSpeed * Time.deltaTime;


            if(Vector2.Distance(transform.position, targetpos) < 0.1f)
            {
                RushEnd();
            }
        }
        else
        {

        }
    }

    protected override void FixedUpdate()
    {

        base.FixedUpdate();
    }

    public void Rush()
    {
        isCatch = true;
        targetpos = playerMovement.gameObject.transform.position;
    }

    public void RushEnd()
    {
        isRush = true;
        animator.SetBool("isCatch", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isRush)
        {
            animator.SetBool("isCatch", true);
        }
    }
}
