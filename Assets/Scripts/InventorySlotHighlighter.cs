using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float scaleUpFactor = 1.2f;
    public float scaleSpeed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isHovered = false;

    void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleUpFactor;
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        isHovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }
}
