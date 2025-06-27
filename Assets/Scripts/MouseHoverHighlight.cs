using UnityEngine;

public class MouseHoverHighlightURP : MonoBehaviour
{
    public Color highlightColor = Color.yellow;

    private Material material;
    private Color originalEmissionColor;
    private bool isHighlighted = false;
    public GameObject canvas;

    void Start()
    {
        material = GetComponent<Renderer>().material;

        if (!material.IsKeywordEnabled("_EMISSION"))
            material.EnableKeyword("_EMISSION");

        // Assegura’t que té emissivitat configurada
        originalEmissionColor = material.GetColor("_EmissionColor");

        canvas.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
        {
            if (!isHighlighted)
                Highlight(true);
        }
        else
        {
            if (isHighlighted)
                Highlight(false);
        }
    }

    void Highlight(bool enable)
    {
        if (enable)
        {
            material.SetColor("_EmissionColor", highlightColor);
            isHighlighted = true;
            canvas.SetActive(true);
        }
        else
        {
            material.SetColor("_EmissionColor", originalEmissionColor);
            isHighlighted = false;
            canvas.SetActive(false);
        }
    }
}
