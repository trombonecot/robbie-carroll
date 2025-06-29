using UnityEngine;

public class MouseHoverCursor : MonoBehaviour
{
    public static MouseHoverCursor Instance;

    [Header("Cursor Textures")]
    public Texture2D defaultCursor;
    public Texture2D hoverCursor;

    [Header("Cursor Settings")]
    public Vector2 hotspot = Vector2.zero;

    private bool isHovering = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (ItemDragHandler.Instance != null && ItemDragHandler.Instance.currentDraggedItem != null)
        {
            if (isHovering)
            {
                ResetCursor();
            }
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                if (!isHovering)
                {
                    Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
                    isHovering = true;
                }
                return;
            }
        }

        if (isHovering)
        {
            ResetCursor();
        }
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        isHovering = false;
    }

    private void OnDisable()
    {
        ResetCursor();
    }
}
