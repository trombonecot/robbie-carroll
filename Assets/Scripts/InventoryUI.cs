using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public Image[] slotImages = new Image[6];

    void Start()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < playerInventory.items.Count)
            {
                slotImages[i].sprite = playerInventory.items[i].icon;
                slotImages[i].enabled = true;
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].enabled = false;
            }
        }
    }
}
