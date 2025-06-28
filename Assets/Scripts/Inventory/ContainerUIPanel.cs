
using System.Diagnostics;

public class ContainerPanelUI : BaseInventoryUI
{
    private bool initiated = false;

    public void ShowItems(Inventory inventory)
    {
        UpdateInventoryUI(inventory);
    }
    
    public void UpdateInventoryUI(Inventory inventory)
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
