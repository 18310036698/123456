using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : MonoBehaviour
{
    //物品数据和要保存到的物品仓库
    public Item item;
    public Inventory mainInventory;

    public void AddNewItem()
    {
        //不存在就找到最前面的一个空位然后填入物品到仓库，存在就物品数量加1
        if (!mainInventory.itemList.Contains(item))
        {
            for (int i = 0; i < mainInventory.itemList.Count; i++)
            {
                if (mainInventory.itemList[i] == null)
                {
                    mainInventory.itemList[i] = item;
                    item.itemHeldNum = 1;
                    break;
                }
            }
        }
        else
        {
            item.itemHeldNum++;
        }

        InventoryManager.RefreshItem();     //刷新UI的物品数
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰到玩家则添加物品
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }
}
