using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    // Add an item to the inventory
    public void AddItem(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log($"Added item: {item.itemName}");
        }
        else
        {
            Debug.Log($"Item already in inventory: {item.itemName}");
        }
    }

    // Remove an item from the inventory
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"Removed item: {item.itemName}");
        }
        else
        {
            Debug.Log($"Item not found: {item.itemName}");
        }
    }

    // Debug function to list all inventory items
    public void ListInventory()
    {
        Debug.Log("Inventory Items:");
        foreach (var item in items)
        {
            Debug.Log($"- {item.itemName}");
        }
    }
}
