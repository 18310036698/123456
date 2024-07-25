using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeldNum;    //持有的物品数
    [TextArea]              //使得输入为多行文本框而不是单行
    public string itemInfo; //物品介绍
    public ItemType type;
}

//物品种类枚举，用于判断物品特性
public enum ItemType
{
    key,
    food,
    equipment
}

