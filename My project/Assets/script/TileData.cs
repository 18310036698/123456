using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// 举例地形类型
/// 决定瓦片存什么信息
/// </summary>
public enum TileTerrain
{
    sea,
    grass,
    mountain,
    dirt,
    defult
}
public class TileData
{
    public bool player;
    public bool enemy;
    public bool isResoucepoint;
    public TileTerrain terraintype;
    //[SerializeField] TileBase tileBase;
    [SerializeField] Sprite sprite;
    //构造函数
    public TileData(Vector3Int vector3, Tilemap map)
    {
        player = false;
        enemy = false;
        isResoucepoint = false;

        //使用GetTile()获取瓦片类型
        //tileBase = map.GetTile(vector3);
        //tileBase.GetTileData(vector3,map,ref tileData);

        //使用Tilemap.GetSprite()获取瓦片类型
        sprite = map.GetSprite(vector3);

        //将瓦片类型赋给terraintype
        if (sprite.name == "Grass(sprite)") { terraintype = TileTerrain.grass; }
        else if (sprite.name == "Dirt(sprite)") { terraintype = TileTerrain.dirt; }
        else if (sprite.name == "Mountain(sprite)") { terraintype = TileTerrain.mountain; }
        else if (sprite.name == "Sea(sprite)") { terraintype = TileTerrain.sea; }
        else { terraintype = TileTerrain.defult; }
    }
    
}
