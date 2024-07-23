using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject tilePrefab; // ��ĸ���Ԥ����
    public int rows = 90; // ����
    public int cols = 9; // ����
    public float spacing = 0f; // ����֮��ļ��

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