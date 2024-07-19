using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
}