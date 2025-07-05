using UnityEngine;
using static UnityEngine.Rendering.BoolParameter;

public class EscenStarter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DialogPanel.Instance != null)
        {
            DialogPanel.Instance.DisplayDialog(Translator.Instance.Translate("scene1.monologue.start"), 5);
        }
        else
        {
            Debug.LogWarning("DialogPanel.Instance is not set.");
        }
    }

}
