using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject tilePrefab; // 你的格子预制体
    public int rows = 90; // 行数
    public int cols = 9; // 列数
    public float spacing = 0f; // 格子之间的间距

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