using UnityEngine;
using UnityEngine.EventSystems;

//���ص���Ʒ���ϣ�ʵ����קЧ��
public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;    //��¼��ʼ�ĸ���λ��
    public Inventory mainInventory;
    public Inventory toolbarInventory;
    Inventory inventory1;
    Inventory inventory2;
    private int curItemID;


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        curItemID = originalParent.GetComponent<Slot>().slotID;

        transform.SetParent(transform.parent.parent);   //���Ĳ㼶��ϵΪ�������ֵܣ���ֹ��Ⱦ����ס
        transform.position = eventData.position;    //λ��������ƶ�
        //��קʱ���Ƶ���Ʒ�ᵲס���ߵĴ�͸��������Ҫ����������ײ������Ʒ�����жϣ�����Ҫ�ص�
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name); //��ʾ���������ײ������������
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject itemOnRaycast = eventData.pointerCurrentRaycast.gameObject;
        Transform itemTransOnRaycast = eventData.pointerCurrentRaycast.gameObject.transform;

        //����ק�յ��������Ʒʱ������������Ʒ��λ��
        if (itemOnRaycast != null)
        {
            if (itemOnRaycast.CompareTag("Item") || itemOnRaycast.CompareTag("Tool Bar Item"))
            {
                //�ı丸����λ��
                transform.SetParent(itemTransOnRaycast.parent);     //itemTransOnRaycast.parent���Ǹ���
                transform.position = itemTransOnRaycast.parent.position;

                //�ж�inventory����
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
                
                //ˢ��itemList����Ʒ�洢λ��
                var temp = inventory1.itemList[curItemID];
                inventory1.itemList[curItemID] = inventory2.itemList[itemOnRaycast.GetComponentInParent<Slot>().slotID];
                inventory2.itemList[itemOnRaycast.GetComponentInParent<Slot>().slotID] = temp;

                itemTransOnRaycast.position = originalParent.position;
                itemTransOnRaycast.SetParent(originalParent);

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            //û����Ʒʱ�����䵽���Ǹ����ӣ�ֱ�Ӹ�ֵ���������
            else if (itemOnRaycast.CompareTag("Slot") || itemOnRaycast.CompareTag("Tool Bar Slot"))
            {
                transform.SetParent(itemTransOnRaycast);
                transform.position = itemTransOnRaycast.position;

                //�ж�
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

                //�ո��ӵ�λ��Ҳ�û�
                itemTransOnRaycast.Find("Item").position = originalParent.position;
                itemTransOnRaycast.Find("Item").SetParent(originalParent);
                inventory2.itemList[itemOnRaycast.GetComponent<Slot>().slotID] = inventory1.itemList[curItemID];
                //��λ���˵Ļ��ͽ�ԭ���ĸ�Ϊ��
                if (itemOnRaycast.GetComponent<Slot>().slotID != curItemID || inventory1 != inventory2)
                    inventory1.itemList[curItemID] = null;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }

        //�䵽��������ʱ�ص�ԭλ��
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}