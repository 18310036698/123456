using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInHand : MonoBehaviour
{
    public Slot slotSelected;     //被框住的格子
    public Inventory inventory; //在哪个物品栏选

    public void CancelSelect()
    {
        slotSelected.caseOfSlot.SetActive(false);
    }
    public void BeSelect()
    {
        slotSelected.caseOfSlot.SetActive(true);
    }
}
