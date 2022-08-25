using UnityEngine;

public class TestPlayer : MonoBehaviour
{
	public int speed;
	public float power;

	private Rigidbody2D rigid;

	public TestKeyLimit limit;
	public TestGameManager gameManager;

	public bool isGround;
	private bool jump;
	private bool jumpFlag;
	private bool isPower;
	private bool isWall;

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

		if (Input.GetKeyDown(KeyCode.Space) && (isGround || isWall) && limit.GetCount(KeyCode.Space) > 0)
		{
			jump = true;
			jumpFlag = true;
		}
		if (Input.GetKeyUp(KeyCode.Space) && jump)
		{
			jump = false;
			limit.DiscountKey(KeyCode.Space);
		}

		float x = Input.GetAxisRaw("Horizontal");
		if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) && limit.GetCount(KeyCode.LeftArrow) > 0 && x <= 0)
		{
			limit.DiscountKey(KeyCode.LeftArrow);
		}
		if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) && limit.GetCount(KeyCode.RightArrow) > 0 && x >= 0)
		{
			limit.DiscountKey(KeyCode.RightArrow);
		}
	}

	private void FixedUpdate()
	{
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

		RaycastHit2D groundRay = Physics2D.Raycast(rigid.position, rayPath, 0.5f, LayerMask.GetMask("Ground"));
		isGround = groundRay.collider != null;

		RaycastHit2D boostRay = Physics2D.Raycast(rigid.position, rayPath, 0.5f, LayerMask.GetMask("Power"));
		if (boostRay.collider != null)
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
		if (x < 0 && limit.GetCount(KeyCode.LeftArrow) > 0)
		{
			rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
		}
		else if (x > 0 && limit.GetCount(KeyCode.RightArrow) > 0)
		{
			rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
		}

		if (jumpFlag && isGround && limit.GetCount(KeyCode.Space) > 0)
		{
			rigid.velocity += Vector2.up * power;
			isGround = false;
			jumpFlag = false;
		}


		if (jumpFlag && !isGround & limit.GetCount(KeyCode.Space) > 0)
		{
			rigid.velocity += Vector2.up * power;
			jumpFlag = false;
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
		if (TestGameManager.dead) return;
		if (gameManager.GameOver) return;

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
