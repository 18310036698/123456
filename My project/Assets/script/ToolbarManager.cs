using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 使背包中的数据显示在UI的背包格子里
/// </summary>
public class ToolbarManager : MonoBehaviour
{
    //用单例模式 方便管理
    static ToolbarManager instance;

    public Inventory toolbar;
    public GameObject slotParent;   //格子的爹  
    public GameObject emptySlot;    //空格子

    public List<GameObject> slotsList = new List<GameObject>();     //用来存放空格子的列表

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
        RefreshItem();      //游戏开始时刷新背包
    }

    //刷新当前背包的物品数：先删后创等于刷新
    public static void RefreshItem()
    {
        //根据子类的数量遍历删除
        for (int i = 0; i < instance.slotParent.transform.childCount; i++)
        {
            if (instance.slotParent.transform.childCount == 0)
                break;
            Destroy(instance.slotParent.transform.GetChild(i).gameObject);
            instance.slotsList.Clear(); //清空列表
        }

        //重新创建物品
        for (int i = 0; i < instance.toolbar.itemList.Count; i++)
        {
            //生成空格子且添加到空格子列表中
            GameObject newSlot = Instantiate(instance.emptySlot);
            //newSlot.transform.localScale = new Vector2(2, 2);
            instance.slotsList.Add(newSlot);
            instance.slotsList[i].transform.SetParent(instance.slotParent.transform);   //静态方法的变量一定是静态值
            instance.slotsList[i].GetComponent<Slot>().slotID = i;  //赋予编号
            instance.slotsList[i].GetComponent<Slot>().SetupSlot(instance.toolbar.itemList[i]);   //赋值生成物品的数据
        }
    }

}
