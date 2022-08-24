using UnityEngine;

public class TestPlayer : MonoBehaviour
{
	public int speed;
	public float power;

	private Rigidbody2D rigid;

	public TestKeyLimit testKeyLimit;
	public TestGameManager gameManager;

	public bool isGround = true;
	private bool jump = false;
	private bool jumpFlag = false;
	private bool isPower = false;
	public bool isWall = false;

	private bool wallJump = false;

	private Vector2 rayPath;

	private void Awake()
	{
		rayPath = Vector2.down;
		rigid = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

		if (Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(gameManager.Dead(true));
		}

		if (Input.GetKeyDown(KeyCode.Space) && isGround && testKeyLimit.GetCount(KeyCode.Space) > 0)
		{
			jump = true;
			jumpFlag = true;
		}
		if (Input.GetKeyUp(KeyCode.Space) && jump)
		{
			jump = false;
			testKeyLimit.DiscountKey(KeyCode.Space);
		}

		if (Input.GetKeyUp(KeyCode.Space) && isWall && testKeyLimit.GetCount(KeyCode.Space) > 0)
		{
			wallJump = true;
			testKeyLimit.DiscountKey(KeyCode.Space);
		}

		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) && testKeyLimit.GetCount(KeyCode.LeftArrow) > 0)
		{
			testKeyLimit.DiscountKey(KeyCode.LeftArrow);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) && testKeyLimit.GetCount(KeyCode.RightArrow) > 0)
		{
			testKeyLimit.DiscountKey(KeyCode.RightArrow);
		}
	}

	private void FixedUpdate()
	{
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

		RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, rayPath, 0.5f, LayerMask.GetMask("Ground")); //ray ½î±â
		isGround = rayHit.collider != null;

		RaycastHit2D rayHit1 = Physics2D.Raycast(rigid.position, rayPath, 0.5f, LayerMask.GetMask("Power")); //ray ½î±â
		if (rayHit1.collider != null)
		{
			isGround = true;
			if (!isPower)
			{
				isPower = true;
				power *= 1.4f;
			}
		}
		else
		{
			if (isPower)
			{
				isPower = false;
				power /= 1.4f;
			}
		}

		float x = Input.GetAxisRaw("Horizontal");
		if (x < 0 && testKeyLimit.GetCount(KeyCode.LeftArrow) > 0)
		{
			rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
		}
		else if (x > 0 && testKeyLimit.GetCount(KeyCode.RightArrow) > 0)
		{
			rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
		}

		if (jumpFlag && isGround && testKeyLimit.GetCount(KeyCode.Space) > 0)
		{
			rigid.velocity += Vector2.up * power;
			isGround = false;
			jumpFlag = false;
		}


		if (wallJump)
		{
			// º®³Ñ±â È½¼ö 2¹ø ÁÙ°í ¾ÆÁ÷ ¿Ï¼º ´ú µÊ!
			rigid.velocity += Vector2.up * power;
			wallJump = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

		if (collision.transform.CompareTag("Enemy"))
		{
			StartCoroutine(gameManager.Dead(true));
		}
		if (collision.transform.CompareTag("Trap"))
		{
			StartCoroutine(gameManager.Dead(true));
		}

		if (!isGround && collision.gameObject.layer == 6)
		{
			isWall = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (!isGround && collision.gameObject.layer == 6)
		{
			isWall = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

		if (collision.CompareTag("Trap"))
		{
			StartCoroutine(gameManager.Dead(true));
		}
		if (collision.CompareTag("Gravity"))
		{
			rigid.gravityScale *= -1;
			power *= -1;
			rayPath = Vector2.up;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

		if (collision.CompareTag("Gravity"))
		{
			rigid.gravityScale *= -1;
			power *= -1;
			rayPath = Vector2.down;
		}
	}
}
