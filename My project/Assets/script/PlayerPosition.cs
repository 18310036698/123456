using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 记录玩家在大地图坐标的单例
/// </summary>

public class PlayerPosition : Singleton<PlayerPosition>
{
    public Vector3 playerPosition = new Vector3(0.5f, 0.5f, 0);
}
