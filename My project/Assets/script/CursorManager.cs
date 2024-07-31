using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����¼�����ű�
/// </summary>
public class CursorManager : MonoBehaviour
{
    // ����������� => C# 6 ��������Ա��ʽ���﷨ Ĭ��ֻ��
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    // �ж��Ƿ���Ե��
    private bool canClick;

    private bool inWorldmap = true;

    public GameObject canvas;

    public GameObject worldmapSencetransform;

    private void Update()
    {
        if (Camera.main != null)
        {
            // �ж��Ƿ��г���ת����ײ��
            canClick = MouseCollider();
            if (canClick && Input.GetMouseButtonDown(0))
            {
                // ִ�������ײ������
                ClickAction(MouseCollider().gameObject); // MouseCollider().gameObject �������ص���ײ��
            }
            
            if (Input.GetButtonDown("Jump") && inWorldmap)
            {
                //�������ͼ��ҿ��ƽű���ȡ������ڸ�����Ϣ
                TileData tileData = FindObjectOfType<WorldMapPlayerAction>().tiledata;
                KeyBoardAction(tileData);
                inWorldmap = false;
            }
            
        }

    }

    /// <summary>
    /// ���������ײ �õ��Ƿ������ײ��
    /// </summary>
    /// <returns></returns>
    private Collider2D MouseCollider()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }


    /// <summary>
    /// �����ײ����
    /// </summary>
    /// <param name="clickObj">��⵽����ײ��</param>
    private void ClickAction(GameObject clickObj)
    {
        // ��ײ������ı�ǩ
        switch (clickObj.tag)
        {
            
            case "Scene":
                // ��ȡ���ϵĽű����� var ��������������ʽָ����������������
                var scene = clickObj.GetComponent<SceneTransition>();
                
                // ִ�к���
                canvas.SetActive(true);
                scene.MoveScene();
                inWorldmap = true;
                break;
        }
    }

    private void KeyBoardAction(TileData tiledata) 
    {
        worldmapSencetransform.GetComponent<SceneTransition>().currentScene = SceneEnums.WorldMap;
        if (tiledata.terraintype == TileTerrain.grass) 
        {
            worldmapSencetransform.GetComponent<SceneTransition>().targetScene = SceneEnums.Area1;
            var scene = worldmapSencetransform.GetComponent<SceneTransition>();
            canvas.SetActive(true);
            scene.MoveScene();
        }
        else if(tiledata.terraintype == TileTerrain.dirt)
        {
            worldmapSencetransform.GetComponent<SceneTransition>().targetScene = SceneEnums.Area2;
            var scene = worldmapSencetransform.GetComponent<SceneTransition>();
            canvas.SetActive(true);
            scene.MoveScene();
        }
        else if (tiledata.terraintype == TileTerrain.mountain)
        {
            worldmapSencetransform.GetComponent<SceneTransition>().targetScene = SceneEnums.Area3;
            var scene = worldmapSencetransform.GetComponent<SceneTransition>();
            canvas.SetActive(true);
            scene.MoveScene();
        }
        else if (tiledata.terraintype == TileTerrain.sea)
        {
            worldmapSencetransform.GetComponent<SceneTransition>().targetScene = SceneEnums.Area4;
            var scene = worldmapSencetransform.GetComponent<SceneTransition>();
            canvas.SetActive(true);
            scene.MoveScene();
        }
    }
}