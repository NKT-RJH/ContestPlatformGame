using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : KinematicObject
{
    //이동 관련
    public float speed;

    //입력 관련
    public bool inputCheckStart;
    public Vector2 input;
    public List<KeyLimit> inputkeylimit = new List<KeyLimit>();

    //점프 관련
    public float jumpPower;
    public float nowJumpPower;
    public float gravityMultifire;
    public bool isGround;
    public LayerMask groundLayer;
    public ContactFilter2D contactFilters = new ContactFilter2D();
    public AudioClip jumpSound;

    //오디오
    public AudioSource playerAudio;

    //콜라이더
    public Collider2D collider2d;

    public bool inputLock;


    protected override void OnEnable()
    {
        playerAudio = FindObjectOfType<AudioSource>();
        collider2d = GetComponent<Collider2D>();
        nowSpeed = speed;
        nowJumpPower = jumpPower;

        contactFilters.layerMask = groundLayer;
        contactFilters.useLayerMask = true;

        inputLock = false;

       
        base.OnEnable();
    }

    private void Start()
    {
        
    }

    protected void Update()
    {

        if (inputLock)
        {
            input.x = 0; input.y = rigid.velocity.y;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < inputkeylimit.Count; i++)
                {
                    if (inputkeylimit[i].key == KeyCode.Space && inputkeylimit[i].times > 0)
                    {
                        Debug.Log("Space");
                        Jump(inputkeylimit[i]);
                    }
                }
            }

            if (inputCheckStart)
            {
                InputEndCheck(KeyCode.RightArrow);
                InputEndCheck(KeyCode.LeftArrow);
            }
            else
            {
                foreach (KeyLimit keyLimit in inputkeylimit)
                {
                    if (InputCheck(keyLimit.key))
                    {
                        inputCheckStart = true;
                    }
                }
            }
            input.x = TwoExisInputCheck(InputCheck(KeyCode.RightArrow), InputCheck(KeyCode.LeftArrow));
        }

        MoveTo(input);
        GroundCheck();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public void Jump(KeyLimit key)
    {
        if (isGround)
        {
            key.times--;
            input.y = nowJumpPower;
            isGround = false;
            playerAudio.PlayOneShot(jumpSound, 0.5f);
        }
    }


    public void GroundCheck()
    {
        if(input.y > 0)
        {
            input.y = input.y - (gravityMultifire * 0.4f);
        }
        else if(rigid.velocity.y < 0 || (rigid.velocity.y == 0) && !isGround)
        {
            RaycastHit2D[] hit = new RaycastHit2D[20];
            int hitColliders = collider2d.Cast(Vector2.down, contactFilters, hit, 0.05f);

            if(hitColliders > 0)
            {
                isGround = true;
                input.y = 0;
            }
            else
            {
                input.y = rigid.velocity.y - gravityMultifire;
                isGround = false;
            }
        }
    }


    public int TwoExisInputCheck(bool a, bool b)
    {
        if(a && b)
        {
            return 0;
        }
        else if (a)
        {
            return 1;
        }
        else if (b)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public bool InputCheck(KeyCode key)
    {
        for (int i = 0; i < inputkeylimit.Count; i++)
        {
            if (inputkeylimit[i].key == key && inputkeylimit[i].times > 0)
            {
                if (Input.GetKey(key))
                {
                    Debug.Log("InputEnd" + key.ToString() + " " + inputkeylimit[i].key.ToString());
                    return true;
                }
            }
        }

        return false;

    }

    public bool InputEndCheck(KeyCode key)
    {

        for (int i = 0; i < inputkeylimit.Count; i++)
        {
            if (inputkeylimit[i].key == key && inputkeylimit[i].times > 0)
            {
                if (Input.GetKeyUp(key))
                {
                    Debug.Log("InputEnd" + key.ToString() + " " + inputkeylimit[i].key.ToString());
                    inputkeylimit[i].times--;
                    return true;
                }
            }
        }
        return false;
        
    }

}
