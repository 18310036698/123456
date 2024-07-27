using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 添加新物品栏（存储物品数据）的菜单
/// 创建新物品栏的时候新建list
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}