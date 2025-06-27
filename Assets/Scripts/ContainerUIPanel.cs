using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContainerPanelUI : MonoBehaviour
{
    public void ShowItems(Item[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Transform slot = transform.GetChild(i);
            Image iconImage = slot.GetChild(0).GetComponent<Image>();

            if (items[i] != null && !string.IsNullOrEmpty(items[i].itemName))
            {
                iconImage.sprite = items[i].icon;
                iconImage.enabled = true;
            }
            else
            {
                iconImage.sprite = null;
                iconImage.enabled = false;
            }
        }
    }
}
