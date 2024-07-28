using UnityEngine;
using UnityEngine.EventSystems;

//挂载到物品身上，实现拖拽效果
public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;    //记录起始的父级位置
    public Inventory mainInventory;
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
            if (itemOnRaycast.CompareTag("Item"))
            {
                //改变父级和位置
                transform.SetParent(itemTransOnRaycast.parent);     //itemTransOnRaycast.parent就是格子
                transform.position = itemTransOnRaycast.parent.position;

                //刷新itemList的物品存储位置
                var temp = mainInventory.itemList[curItemID];
                mainInventory.itemList[curItemID] = mainInventory.itemList[itemOnRaycast.GetComponentInParent<Slot>().slotID];
                mainInventory.itemList[itemOnRaycast.GetComponentInParent<Slot>().slotID] = temp;

                itemTransOnRaycast.position = originalParent.position;
                itemTransOnRaycast.SetParent(originalParent);

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            //没有物品时射线射到的是个格子，直接赋值给这个格子
            else if (itemOnRaycast.CompareTag("Slot"))
            {
                transform.SetParent(itemTransOnRaycast);
                transform.position = itemTransOnRaycast.position;
                //空格子的位置也得换
                itemTransOnRaycast.Find("Item").position = originalParent.position;
                itemTransOnRaycast.Find("Item").SetParent(originalParent);


                mainInventory.itemList[itemOnRaycast.GetComponent<Slot>().slotID] = mainInventory.itemList[curItemID];
                //变位置了的话就将原来的改为空
                if (itemOnRaycast.GetComponent<Slot>().slotID != curItemID)
                    mainInventory.itemList[curItemID] = null;

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