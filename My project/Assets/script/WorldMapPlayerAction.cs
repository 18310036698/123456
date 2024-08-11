using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// �����ͼ�����Ϊ����
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
    //��wasd������Ұ����ƶ�
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
    //��ȡ������ڸ���Ϣ
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
        //ͨ����ǰ�����л�Ծ��EventSystemʵ������ȡ�����¼�������
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        //���ü��ĵ�����
        pointerData.position = pos;
        //��⵽�Ķ����б�
        List<RaycastResult> results = new List<RaycastResult>();
        //����ĵ������⵽������������䵽�б�
        EventSystem.current.RaycastAll(pointerData, results);
        //���û��⵽�κ������򷵻�false
        if (results.Count < 1) return false;
        else
        {
            for (int i = 0; i < 20; i++)
            {
                //��⵽�˵������tagΪSlot��������⵽��Slot������true
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
            //��֮��ⲻ��Slot,����false
            return false;
        }
    }
}
