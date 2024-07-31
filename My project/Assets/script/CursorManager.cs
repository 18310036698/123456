using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鼠标事件管理脚本
/// </summary>
public class CursorManager : MonoBehaviour
{
    // 更换鼠标坐标 => C# 6 引入的属性表达式体语法 默认只读
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    // 判断是否可以点击
    private bool canClick;

    private bool inWorldmap = true;

    public GameObject canvas;

    public GameObject worldmapSencetransform;

    private void Update()
    {
        if (Camera.main != null)
        {
            // 判断是否有场景转移碰撞体
            canClick = MouseCollider();
            if (canClick && Input.GetMouseButtonDown(0))
            {
                // 执行鼠标碰撞处理函数
                ClickAction(MouseCollider().gameObject); // MouseCollider().gameObject 函数返回的碰撞体
            }
            
            if (Input.GetButtonDown("Jump") && inWorldmap)
            {
                //从世界地图玩家控制脚本获取玩家所在格子信息
                TileData tileData = FindObjectOfType<WorldMapPlayerAction>().tiledata;
                KeyBoardAction(tileData);
                inWorldmap = false;
            }
            
        }

    }

    /// <summary>
    /// 返回鼠标碰撞 该点是否存在碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D MouseCollider()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }


    /// <summary>
    /// 鼠标碰撞处理
    /// </summary>
    /// <param name="clickObj">检测到的碰撞体</param>
    private void ClickAction(GameObject clickObj)
    {
        // 碰撞体上面的标签
        switch (clickObj.tag)
        {
            
            case "Scene":
                // 获取身上的脚本代码 var 声明变量而不显式指定变量的数据类型
                var scene = clickObj.GetComponent<SceneTransition>();
                
                // 执行函数
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