using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeraction : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Transform GroundCheck;
    public bool isground;
    public LayerMask env;
    public int jumpcount;
    public float gravity;
    public float fallgravity;
    public float maxjumptime;
    public float jumptime;
    public float jumpforce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jump();
    }
    void FixedUpdate()
    {
        //通过增加速度来实现横向移动
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //实现地面检测
        isground = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, env);
    }
    void jump()
    {
        if (Input.GetButton("Jump"))
        {
            jumptime += Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && jumpcount >= 0)
        {
            if (isground)
            {
                jumpcount = 2;
            }
        }
        if (Input.GetButtonDown("Jump") || isground)
        {
            gravity = 5f;
        }
        if (Input.GetButtonDown("Jump") && jumpcount > 0)
        {
            if (jumpcount < 2 && jumpcount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            rb.gravityScale = gravity;
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            jumpcount--;
            jumptime = 0f;
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (jumptime < maxjumptime)
            {
                gravity = gravity * 1.5f / (0.5f + jumptime);
            }
        }
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = gravity;
        }
        else
        {
            rb.gravityScale = fallgravity;
        }
    }
}
