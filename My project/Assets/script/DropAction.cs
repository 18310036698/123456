using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : MonoBehaviour
{
    //��Ʒ���ݺ�Ҫ���浽����Ʒ�ֿ�
    public Item item;
    public Inventory mainInventory;

    public void AddNewItem()
    {
        //�����ھ��ҵ���ǰ���һ����λȻ��������Ʒ���ֿ⣬���ھ���Ʒ������1
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

        InventoryManager.RefreshItem();     //ˢ��UI����Ʒ��
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��������������Ʒ
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }
}
