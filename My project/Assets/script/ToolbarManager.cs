using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ʹ�����е�������ʾ��UI�ı���������
/// </summary>
public class ToolbarManager : MonoBehaviour
{
    //�õ���ģʽ �������
    static ToolbarManager instance;

    public Inventory toolbar;
    public GameObject slotParent;   //���ӵĵ�  
    public GameObject emptySlot;    //�ո���

    public List<GameObject> slotsList = new List<GameObject>();     //������ſո��ӵ��б�

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();      //��Ϸ��ʼʱˢ�±���
    }

    //ˢ�µ�ǰ��������Ʒ������ɾ�󴴵���ˢ��
    public static void RefreshItem()
    {
        //�����������������ɾ��
        for (int i = 0; i < instance.slotParent.transform.childCount; i++)
        {
            if (instance.slotParent.transform.childCount == 0)
                break;
            Destroy(instance.slotParent.transform.GetChild(i).gameObject);
            instance.slotsList.Clear(); //����б�
        }

        //���´�����Ʒ
        for (int i = 0; i < instance.toolbar.itemList.Count; i++)
        {
            //���ɿո�������ӵ��ո����б���
            GameObject newSlot = Instantiate(instance.emptySlot);
            //newSlot.transform.localScale = new Vector2(2, 2);
            instance.slotsList.Add(newSlot);
            instance.slotsList[i].transform.SetParent(instance.slotParent.transform);   //��̬�����ı���һ���Ǿ�ֵ̬
            instance.slotsList[i].GetComponent<Slot>().slotID = i;  //������
            instance.slotsList[i].GetComponent<Slot>().SetupSlot(instance.toolbar.itemList[i]);   //��ֵ������Ʒ������
        }
    }

}
