using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TileTerrain
{
    sea,
    grass,
    montain,
    dirt,
    defult
}
public class TileData : MonoBehaviour
{
    public bool player;
    public bool enemy;
    public TileTerrain terraintype;
    //构造函数
    //使用TileMap.GetTile()获取瓦片类型
}
