using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������Ʒ�����洢��Ʒ���ݣ��Ĳ˵�
/// ��������Ʒ����ʱ���½�list
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}