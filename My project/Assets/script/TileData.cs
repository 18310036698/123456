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
    //���캯��
    //ʹ��TileMap.GetTile()��ȡ��Ƭ����
}
