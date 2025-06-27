using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;

    private Item heldItem = null;
    private InventorySlotUI sourceSlot = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PickUpItem(Item item, InventorySlotUI fromSlot)
    {
        heldItem = item;
        sourceSlot = fromSlot;

        fromSlot.ClearSlot(); // Remove from original slot
    }

    public void DropItem(InventorySlotUI toSlot)
    {
        if (heldItem == null)
            return;

        // If dropping on same slot, put it back
        if (toSlot == sourceSlot)
        {
            toSlot.SetItem(heldItem);
        }
        else
        {
            // Swap items
            Item temp = toSlot.GetItem();
            toSlot.SetItem(heldItem);
            if (sourceSlot != null)
                sourceSlot.SetItem(temp);
        }

        ClearHeldItem();
    }

    public void ClearHeldItem()
    {
        heldItem = null;
        sourceSlot = null;
    }

    public bool IsHoldingItem()
    {
        return heldItem != null;
    }

    public Item GetHeldItem()
    {
        return heldItem;
    }
}
