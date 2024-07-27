using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 测试脚本，用来代替tilemap生成网格地图
/// </summary>
public class CreateMap : MonoBehaviour
{
    public GameObject tilePrefab; // 你的格子预制体
    public int rows = 5; // 行数
    public int cols = 10; // 列数
    public float spacing = 0.2f; // 格子之间的间距

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y > -cols; y--)
            {
                Vector3 position = new Vector3(x * (tilePrefab.transform.localScale.x + spacing) - 30, y * (tilePrefab.transform.localScale.y + spacing) -2.3f, 0);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
                Debug.Log(position);
            }
        }
    }
}