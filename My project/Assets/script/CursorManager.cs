using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject canvas;

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
                //canvas.SetActive(false);
                break;
        }
    }


}