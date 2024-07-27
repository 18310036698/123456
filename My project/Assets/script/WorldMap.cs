using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// 开始时在字典中录入世界地图瓦片类型与坐标
/// </summary>
public class WorldMap : MonoBehaviour
{
    public Dictionary<Vector2Int, TileData> dict = new Dictionary<Vector2Int, TileData>();
    [SerializeField] Tilemap map;
    private void Awake()
    {
        //字典存瓦片坐标和data
        for (int x = -9; x <= 9; x++) 
        {
            for (int y = -5; y <= 5; y++) 
            {
                Vector3Int vector3 = new Vector3Int(x, y, 0);
                dict.Add(new Vector2Int(x,y),new TileData(vector3, map));
            }
        }
    }
}
