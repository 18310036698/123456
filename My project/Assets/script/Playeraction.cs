using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// 控制区域中玩家行为
/// </summary>
public class PlayerAction : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float speedLimit;
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
    public float hurtForce;
    public int atk;
    public int atkForce;
    bool isOpen = false;
    bool isPause = false;
    [SerializeField] GameObject bag;
    [SerializeField] GameObject pauseMenu;
    public InputAction openbag;
    GameObject objectSlot = null;
    GameObject toolbarSlot = null;
    GameObject buttononPauseMenu = null;
    ItemSelect lastitemSelect = null;
    ItemInHand lastiteminhand = null;
    public Slot iteminhand;
    public int health { get { return currentHealth; } }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        atkArea1.SetActive(false);
        atkArea2.SetActive(false);
        bag.SetActive(isOpen);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            Jump();
            Attack();
            Invincible();
            Dig();
            OpenBag();
        }
        Pause();
        SelectItem();
        ItemHandel();
        ClickResume();
        //Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Color.red);
    }
    void FixedUpdate()
    {
        Move();
        //实现地面检测
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, env);
    }
    void Move()
    {
        //通过增加速度来实现横向移动
        float horizontal = Input.GetAxis("Horizontal");
        if (rb.velocity.x < speedLimit && rb.velocity.x > -speedLimit) 
        {
            rb.velocity = new Vector2(horizontal * speed + rb.velocity.x, rb.velocity.y);
        }
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
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0)
        {
            face = true;
        }
        else if (horizontal < 0)
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
    public void Hurt(bool hurt)
    {
        if (hurt)
        {
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * hurtForce, ForceMode2D.Impulse);
        }
        else 
        {
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            rb.AddForce(Vector2.left * hurtForce, ForceMode2D.Impulse);
        }
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
    void Dig() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 3);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "can dig" && Mathf.Abs(hit.transform.position.x - transform.position.x) < 3)
                {
                    ResourceAction resourceAction = hit.collider.gameObject.GetComponent<ResourceAction>();
                    resourceAction.beDestroy();
                    Debug.Log("射线检测：" + hit.collider.name);
                }
            }
        }
    }
    void SelectItem()
    {
        if (Input.GetMouseButtonDown(0) && IsOnSlot(Input.mousePosition))
        {
            Slot slot = objectSlot.GetComponent<Slot>();
            if (slot.itemInSlot != null && lastitemSelect != slot.itemInSlot.GetComponent<ItemSelect>())
            {
                ItemSelect itemSelect = slot.itemInSlot.GetComponent<ItemSelect>();
                itemSelect.BeSelect();
                if (lastitemSelect != null)
                {
                    lastitemSelect.CancelSelect();
                }
                lastitemSelect = slot.itemInSlot.GetComponent<ItemSelect>();
            }
        }
    }
    void ItemHandel()
    {
        if (Input.GetMouseButtonDown(0) && IsOnToolBar(Input.mousePosition))
        {
            Slot slot = toolbarSlot.GetComponent<Slot>();
            iteminhand = slot;
            if (slot.itemInSlot != null && lastiteminhand != slot.itemInSlot.GetComponent<ItemInHand>())
            {
                ItemInHand iteminhand = slot.itemInSlot.GetComponent<ItemInHand>();
                iteminhand.BeSelect();
                if (lastiteminhand != null)
                {
                    lastiteminhand.CancelSelect();
                }
                lastiteminhand = slot.itemInSlot.GetComponent<ItemInHand>();
            }
        }
    }
    void ClickResume()
    {
        if (Input.GetMouseButtonDown(0) && IsOnResume(Input.mousePosition))
        {
            isPause = !isPause;
            pauseMenu.SetActive(isPause);
            Time.timeScale = 1;
        }
    }
    bool IsOnSlot(Vector2 pos)
    {
        //通过当前场景中活跃的EventSystem实例，获取输入事件的数据
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        //设置检测的点坐标
        pointerData.position = pos;
        //检测到的对象列表
        List<RaycastResult> results = new List<RaycastResult>();
        //传入的点坐标检测到的所有物体填充到列表
        EventSystem.current.RaycastAll(pointerData, results);
        //如果没检测到任何物体则返回false
        if (results.Count < 1) return false;
        else
        {
            for (int i = 0; i < 20; i++)
            {
                //检测到了的物体的tag为Slot，则代表检测到了Slot，返回true
                if (results[i].gameObject.tag == "Slot")
                {
                    objectSlot = results[i].gameObject;
                    return true;
                }
                else if (results[i].gameObject.tag == "bottom of UI")
                {
                    break;
                }
            }
            //反之检测不是Slot,返回false
            return false;
        }
    }
    bool IsOnToolBar(Vector2 pos)
    {
        //通过当前场景中活跃的EventSystem实例，获取输入事件的数据
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        //设置检测的点坐标
        pointerData.position = pos;
        //检测到的对象列表
        List<RaycastResult> results = new List<RaycastResult>();
        //传入的点坐标检测到的所有物体填充到列表
        EventSystem.current.RaycastAll(pointerData, results);
        //如果没检测到任何物体则返回false
        if (results.Count < 1) return false;
        else
        {
            for (int i = 0; i < 20; i++)
            {
                //检测到了的物体的tag为Slot，则代表检测到了Slot，返回true
                if (results[i].gameObject.tag == "Tool Bar Slot")
                {
                    toolbarSlot = results[i].gameObject;
                    return true;
                }
                else if (results[i].gameObject.tag == "bottom of UI")
                {
                    break;
                }
            }
            //反之检测不是Slot,返回false
            return false;
        }
    }
    bool IsOnResume(Vector2 pos)
    {
        //通过当前场景中活跃的EventSystem实例，获取输入事件的数据
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        //设置检测的点坐标
        pointerData.position = pos;
        //检测到的对象列表
        List<RaycastResult> results = new List<RaycastResult>();
        //传入的点坐标检测到的所有物体填充到列表
        EventSystem.current.RaycastAll(pointerData, results);
        //如果没检测到任何物体则返回false
        if (results.Count < 1) return false;
        else
        {
            for (int i = 0; i < 20; i++)
            {
                //检测到了的物体的tag为Slot，则代表检测到了Slot，返回true
                if (results[i].gameObject.tag == "resume")
                {
                    buttononPauseMenu = results[i].gameObject;
                    return true;
                }
                else if (results[i].gameObject.tag == "bottom of UI")
                {
                    break;
                }
            }
            //反之检测不是Slot,返回false
            return false;
        }
    }
    public void OpenBag()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;   //赋反值
            bag.SetActive(isOpen);
        }
    }
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P) && isPause == false)
        {
            isPause = true;
            pauseMenu.SetActive(isPause);
            Time.timeScale = 0;
        }
    }
}

