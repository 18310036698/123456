using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// ������������
/// ������Ƭ��ʲô��Ϣ
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
    //���캯��
    public TileData(Vector3Int vector3, Tilemap map)
    {
        player = false;
        enemy = false;
        isResoucepoint = false;

        //ʹ��GetTile()��ȡ��Ƭ����
        //tileBase = map.GetTile(vector3);
        //tileBase.GetTileData(vector3,map,ref tileData);

        //ʹ��Tilemap.GetSprite()��ȡ��Ƭ����
        sprite = map.GetSprite(vector3);

        //����Ƭ���͸���terraintype
        if (sprite.name == "Grass(sprite)") { terraintype = TileTerrain.grass; }
        else if (sprite.name == "Dirt(sprite)") { terraintype = TileTerrain.dirt; }
        else if (sprite.name == "Mountain(sprite)") { terraintype = TileTerrain.mountain; }
        else if (sprite.name == "Sea(sprite)") { terraintype = TileTerrain.sea; }
        else { terraintype = TileTerrain.defult; }
    }
    
}
