using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Transform groundCheck;
    [SerializeField]private bool isGrounded;
    public LayerMask env;
    public int jumpCount;
    public float gravity;
    public float fallGravity;
    public float maxJumptime;
    public float jumpTime;
    public float jumpForce;
    public float speedx;
    public bool face=true;
    public float atkTime=0;
    public float duration;
    public float coolDown;
    public GameObject atkArea1, atkArea2;
    public bool isInvincible, isAtkable, atked;
    public int currentHealth;
    public int maxHealth;
    public float timeInvincible = 2.0f, timeAtkable = 1.0f;
    float invincibleTimer;
    public int health { get { return currentHealth; } }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        atkArea1.SetActive(false);
        atkArea2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
        Invincible();
    }
    void FixedUpdate()
    {
        //通过增加速度来实现横向移动
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //实现地面检测
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, env);
    }
    void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            jumpTime += Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && jumpCount >= 0)
        {
            if (isGrounded)
            {
                jumpCount = 2;
            }
        }
        if (Input.GetButtonDown("Jump") || isGrounded)
        {
            gravity = 5f;
        }
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            if (jumpCount < 2 && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            rb.gravityScale = gravity;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount--;
            jumpTime = 0f;
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (jumpTime < maxJumptime)
            {
                gravity = gravity * 1.5f / (0.5f + jumpTime);
            }
        }
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = gravity;
        }
        else
        {
            rb.gravityScale = fallGravity;
        }
    }
    bool Face()
    {
        speedx = rb.velocity.x;
        if (speedx > 0)
        {
            face = true;
        }
        else if (speedx < 0)
        {
            face = false;
        }
        return face;
    }
    void Attack()
    {
        bool atkface = Face();
        if (Input.GetButton("Fire1") && atkTime == 0)
        {
            //激活判定区域
            if (atkface)
            {
                //激活右
                atkArea2.SetActive(true);
            }
            else
            {
                //激活左
                atkArea1.SetActive(true);
            }
            atkTime = Time.time;
        }
        if (Time.time-atkTime > duration)
        {
            //de激活判定区域
            atkArea1.SetActive(false);
            atkArea2.SetActive(false);
        }
        if (Time.time - atkTime > coolDown)
        {
            atkTime = 0;
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("Player:" + currentHealth + "/" + maxHealth);
    }
    void Invincible()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }
}
