using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Item item;
    public Image image;
    public TMP_Text text;

    public Inventory inventory;
    public int index;

    public float scaleUpFactor = 1.2f;
    public float scaleSpeed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    void Awake()
    {
        originalScale = image.transform.localScale;
        targetScale = originalScale;
    }
    private void Update()
    {
        image.transform.localScale = Vector3.Lerp(image.transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

        if (item != null)
        {
            image.sprite = item.icon;
            text.text = item.name;
            image.color = Color.white;
        }
        else
        {
            image.sprite = null;
            image.color = new Color(1f, 1f, 1f, 0f);
            text.text = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleUpFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Item draggedItem = ItemDragHandler.Instance.currentDraggedItem;
        Inventory fromInventory = ItemDragHandler.Instance.currentFromInventory;

        if (item == null && draggedItem != null)
        {
            if (fromInventory)
            {
                fromInventory.RemoveItem(draggedItem);
            }
            inventory.AddItem(draggedItem, index);
            ItemDragHandler.Instance.EndDrag();
        }
        else if (item != null && draggedItem == null)
        {
            ItemDragHandler.Instance.StartDrag(item, inventory);
            item = null;
        }
        else if (item != null && draggedItem != null)
        {
            Item temp = item;
            item = draggedItem;
            ItemDragHandler.Instance.StartDrag(temp, inventory);
        }
    }
}
