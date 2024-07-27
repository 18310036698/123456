using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 物品数据存在slot里，slot显示在背包的背景图上面
/// </summary>
public class Slot : MonoBehaviour
{
    public int slotID;      //格子的编号
    public Item slotItem;   //物品的属性
    public Image slotImage;
    public Text slotNum;     //持有数
    public GameObject Description;
    public Text DescriptionText;

    public GameObject itemInSlot;
    //生成格子的数据
    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);    //如果当前格子没物品数据，那就不显示该物品（不然就会显示一张白图）
            return;
        }

        //物品数据显示
        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeldNum.ToString();
        DescriptionText.text = item.itemInfo;
    }
}