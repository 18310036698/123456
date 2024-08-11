using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 世界地图玩家行为控制
/// </summary>
public class WorldMapPlayerAction : MonoBehaviour
{
    public float coolDownTimer;
    public float movingCoolDown;
    bool isPause = false;
    [SerializeField] WorldMap map;
    [SerializeField] GameObject pauseMenu;
    public TileData tiledata;
    public Vector3 startPosition;
    GameObject buttononPauseMenu = null;
    // Start is called before the first frame update
    void Start()
    {
        movingCoolDown = 0.25f;
        coolDownTimer = movingCoolDown;
        transform.position = PlayerPosition.Instance.playerPosition;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            Move();
        }
        Pause();
        ClickResume();
        GetTileData();
    }
    //按wasd控制玩家按格移动
    void Move() 
    {
        if (Input.GetKey("d") && coolDownTimer >= movingCoolDown) 
        {
            transform.position += new Vector3(1, 0, 0);
            coolDownTimer = 0;
        }
        if (Input.GetKey("a") && coolDownTimer >= movingCoolDown)
        {
            transform.position += new Vector3(-1, 0, 0);
            coolDownTimer = 0;
        }
        if (Input.GetKey("w") && coolDownTimer >= movingCoolDown)
        {
            transform.position += new Vector3(0, 1, 0);
            coolDownTimer = 0;
        }
        if (Input.GetKey("s") && coolDownTimer >= movingCoolDown)
        {
            transform.position += new Vector3(0, -1, 0);
            coolDownTimer = 0;
        }
        coolDownTimer += Time.deltaTime;
        PlayerPosition.Instance.playerPosition = transform.position;
    }
    //读取玩家所在格信息
    void GetTileData() 
    {
        Vector2Int playerLocation = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        tiledata = map.dict[playerLocation];
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
    void ClickResume()
    {
        if (Input.GetMouseButtonDown(0) && IsOnResume(Input.mousePosition))
        {
            isPause = !isPause;
            pauseMenu.SetActive(isPause);
            Time.timeScale = 1;
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
}
