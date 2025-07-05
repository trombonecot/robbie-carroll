using UnityEngine;

public class ItemWithInformation : MonoBehaviour
{
    [TextArea]
    public string informationText = "This is an object with information.";
    public float displayTime = 3f;

    private void OnMouseDown()
    {
        if (DialogPanel.Instance != null)
        {
            DialogPanel.Instance.DisplayDialog(Translator.Instance.Translate(informationText), displayTime);
        }
        else
        {
            Debug.LogWarning("DialogPanel.Instance is not set.");
        }
    }
}
