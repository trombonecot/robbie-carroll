using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySlot[] slots;
    public Inventory inventory;

    public void Connect(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public void Disconnect()
    {
        this.inventory = null;
    }

    private void Update()
    {
        if (this.inventory != null)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].index = i;
            slots[i].inventory = inventory;

            if (inventory.items.ContainsKey(i))
            {
                slots[i].item = inventory.items[i];
            }
            else
            {
                slots[i].item = null;
            }
        }
    }

}
