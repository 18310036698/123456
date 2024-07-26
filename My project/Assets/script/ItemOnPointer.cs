using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnPointer : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    public GameObject Description;
    public Text DescriptionText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Description.SetActive(true);
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        Description.transform.position = new Vector3(eventData.position.x+100, eventData.position.y-30, 0);//ŒÔ∆∑√Ë ˆøÚ∏˙ÀÊ Û±Í
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Description.SetActive(false);
    }
}

