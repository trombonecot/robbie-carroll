using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image iconImage;
    private Item currentItem;

    private Transform originalParent;
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void SetItem(Item item)
    {
        currentItem = item;
        iconImage.sprite = item != null ? item.icon : null;
        iconImage.enabled = item != null;
    }

    public void ClearSlot()
    {
        currentItem = null;
        iconImage.sprite = null;
        iconImage.enabled = false;
    }

    public Item GetItem() => currentItem;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        originalParent = iconImage.transform.parent;
        iconImage.transform.SetParent(canvas.transform); // So it can move freely
        iconImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        iconImage.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        iconImage.transform.SetParent(originalParent);
        iconImage.transform.localPosition = Vector3.zero;
        iconImage.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlotUI fromSlot = eventData.pointerDrag?.GetComponent<InventorySlotUI>();
        if (fromSlot != null && fromSlot != this)
        {
            Item temp = currentItem;
            SetItem(fromSlot.GetItem());
            fromSlot.SetItem(temp);
        }
    }
}
