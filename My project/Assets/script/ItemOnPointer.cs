using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 当鼠标放在背包的某个物品上时显示物品描述
/// </summary>
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
        Description.transform.position = new Vector3(eventData.position.x+100, eventData.position.y-30, 0);//物品描述框跟随鼠标
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Description.SetActive(false);
    }
}

