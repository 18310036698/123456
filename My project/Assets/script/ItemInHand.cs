using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInHand : MonoBehaviour
{
    public Slot slotSelected;     //����ס�ĸ���
    public Inventory inventory; //���ĸ���Ʒ��ѡ

    public void CancelSelect()
    {
        slotSelected.caseOfSlot.SetActive(false);
    }
    public void BeSelect()
    {
        slotSelected.caseOfSlot.SetActive(true);
    }
}
