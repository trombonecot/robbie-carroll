using UnityEngine;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour
{
    public static ItemDragHandler Instance;

    public Image dragImage;
    public Item currentDraggedItem;
    public Inventory currentFromInventory;

    private void Awake()
    {
        Instance = this;
        dragImage.raycastTarget = false;
        dragImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (dragImage.gameObject.activeSelf)
        {
            dragImage.transform.position = Input.mousePosition;
        }
    }

    public void StartDrag(Item item, Inventory inventory)
    {
        currentFromInventory = inventory;
        currentDraggedItem = item;
        dragImage.sprite = item.icon;
        dragImage.color = Color.white;
        dragImage.gameObject.SetActive(true);
    }

    public void EndDrag()
    {
        currentDraggedItem = null;
        currentFromInventory = null;
        dragImage.gameObject.SetActive(false);
    }
}
