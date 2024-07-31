using UnityEngine;
using UnityEngine.EventSystems;

//挂载到物品身上，实现拖拽效果
public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;    //记录起始的父级位置
    public Inventory mainInventory;
    public Inventory toolbarInventory;
    Inventory inventory1;
    Inventory inventory2;
    private int curItemID;


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        curItemID = originalParent.GetComponent<Slot>().slotID;

        transform.SetParent(transform.parent.parent);   //更改层级关系为他爹的兄弟，防止渲染被挡住
        transform.position = eventData.position;    //位置随鼠标移动
        //拖拽时控制的物品会挡住射线的穿透，而我们要根据射线碰撞到的物品进行判断，所以要关掉
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name); //显示鼠标射线碰撞到的物体名称
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject itemOnRaycast = eventData.pointerCurrentRaycast.gameObject;
        Transform itemTransOnRaycast = eventData.pointerCurrentRaycast.gameObject.transform;

        //当拖拽终点格子有物品时，交换两个物品的位置
        if (itemOnRaycast != null)
        {
            if (itemOnRaycast.CompareTag("Item") || itemOnRaycast.CompareTag("Tool Bar Item"))
            {
                //改变父级和位置
                transform.SetParent(itemTransOnRaycast.parent);     //itemTransOnRaycast.parent就是格子
                transform.position = itemTransOnRaycast.parent.position;

                //判断inventory种类
                if (itemOnRaycast.CompareTag("Item") && CompareTag("Item"))
                {
                    inventory1 = mainInventory;
                    inventory2 = mainInventory;
                }
                else if (itemOnRaycast.CompareTag("Tool Bar Item") && CompareTag("Item"))
                {
                    inventory1 = mainInventory;
                    inventory2 = toolbarInventory;
                }
                else if (itemOnRaycast.CompareTag("Item") && CompareTag("Tool Bar Item"))
                {
                    inventory1 = toolbarInventory;
                    inventory2 = mainInventory;
                }
                else if (itemOnRaycast.CompareTag("Tool Bar Item") && CompareTag("Tool Bar Item"))
                {
                    inventory1 = toolbarInventory;
                    inventory2 = toolbarInventory;
                }
                
                //刷新itemList的物品存储位置
                var temp = inventory1.itemList[curItemID];
                inventory1.itemList[curItemID] = inventory2.itemList[itemOnRaycast.GetComponentInParent<Slot>().slotID];
                inventory2.itemList[itemOnRaycast.GetComponentInParent<Slot>().slotID] = temp;

                itemTransOnRaycast.position = originalParent.position;
                itemTransOnRaycast.SetParent(originalParent);

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            //没有物品时射线射到的是个格子，直接赋值给这个格子
            else if (itemOnRaycast.CompareTag("Slot") || itemOnRaycast.CompareTag("Tool Bar Slot"))
            {
                transform.SetParent(itemTransOnRaycast);
                transform.position = itemTransOnRaycast.position;

                //判断
                if (itemOnRaycast.CompareTag("Slot") && CompareTag("Item"))
                {
                    inventory1 = mainInventory;
                    inventory2 = mainInventory;
                }
                else if (itemOnRaycast.CompareTag("Tool Bar Slot") && CompareTag("Item"))
                {
                    inventory1 = mainInventory;
                    inventory2 = toolbarInventory;
                    itemTransOnRaycast.Find("Item").tag = "Item";
                    tag = "Tool Bar Item";
                }
                else if (itemOnRaycast.CompareTag("Slot") && CompareTag("Tool Bar Item"))
                {
                    inventory1 = toolbarInventory;
                    inventory2 = mainInventory;
                    itemTransOnRaycast.Find("Item").tag = "Tool Bar Item";
                    tag = "Item";
                }
                else if (itemOnRaycast.CompareTag("Tool Bar Slot") && CompareTag("Tool Bar Item"))
                {
                    inventory1 = toolbarInventory;
                    inventory2 = toolbarInventory;
                }

                //空格子的位置也得换
                itemTransOnRaycast.Find("Item").position = originalParent.position;
                itemTransOnRaycast.Find("Item").SetParent(originalParent);
                inventory2.itemList[itemOnRaycast.GetComponent<Slot>().slotID] = inventory1.itemList[curItemID];
                //变位置了的话就将原来的改为空
                if (itemOnRaycast.GetComponent<Slot>().slotID != curItemID || inventory1 != inventory2)
                    inventory1.itemList[curItemID] = null;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }

        //射到其他东西时回到原位置
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}