using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject canvas;

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
                //canvas.SetActive(false);
                break;
        }
    }


}