using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Custom Data/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private List<string> itemsNames = new List<string>();

    public void AddItem(Item item)
    {
        items.Add(item);
        itemsNames.Add("");
    }
}
