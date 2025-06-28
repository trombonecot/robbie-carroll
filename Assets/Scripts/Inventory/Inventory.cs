using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryEntry
{
    public int position;
    public Item item;
}

public class Inventory : MonoBehaviour
{
    [Header("Editable Inventory")]
    public List<InventoryEntry> initialItems = new List<InventoryEntry>();

    public Dictionary<int, Item> items = new Dictionary<int, Item>();

    private void Awake()
    {
        foreach (var entry in initialItems)
        {
            if (!items.ContainsKey(entry.position) && entry.item != null)
            {
                items[entry.position] = entry.item;
            }
        }
    }

    public void AddItem(Item item, int position)
    {
        if (!items.ContainsValue(item) && !items.ContainsKey(position))
        {
            items[position] = item;
        }
        else
        {
            Debug.LogWarning("Cannot add item. Either item already exists or position is occupied.");
        }
    }

    public void RemoveItem(Item item)
    {
        int positionToRemove = -1;
        foreach (var kvp in items)
        {
            if (kvp.Value == item)
            {
                positionToRemove = kvp.Key;
                break;
            }
        }

        if (positionToRemove != -1)
        {
            items.Remove(positionToRemove);
        }
    }

    public void MoveItem(int fromPosition, int toPosition)
    {
        if (!items.ContainsKey(fromPosition))
        {
            Debug.LogWarning("No item at the source position to move.");
            return;
        }

        Item fromItem = items[fromPosition];

        if (items.ContainsKey(toPosition))
        {
            Item toItem = items[toPosition];
            items[toPosition] = fromItem;
            items[fromPosition] = toItem;
        }
        else
        {
            items[toPosition] = fromItem;
            items.Remove(fromPosition);
        }
    }

    public Item GetItemAtPosition(int position)
    {
        items.TryGetValue(position, out var item);
        return item;
    }
}
