using UnityEngine;

public class InventoryUI : BaseInventoryUI
{
    public Inventory playerInventory;

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].index = i;
            slots[i].inventory = playerInventory;
        }
    }

    private void Update()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (playerInventory.items.ContainsKey(i))
            {
                slots[i].item = playerInventory.items[i];
            }
            else
            {
                slots[i].item = null;
            }
        }
    }
}
