using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapPlayerAction : MonoBehaviour
{
    public float coolDownTimer;
    public float movingCoolDown;
    [SerializeField] WorldMap map;
    public TileData tiledata;
    public Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        movingCoolDown = 0.25f;
        coolDownTimer = movingCoolDown;
        transform.position = PlayerPosition.Instance.playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
}
