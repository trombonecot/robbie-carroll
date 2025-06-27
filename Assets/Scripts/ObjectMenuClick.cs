using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ObjectClickMenu : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas uiCanvas;
    public GameObject menuPrefab;
    public Transform playerTransform;  // Assigna-hi el jugador

    public float maxDistance = 3f;
    public float menuDuration = 3f;

    private GameObject currentMenu;
    private Coroutine hideCoroutine;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Evita clicar a través d'elements UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                float distance = Vector3.Distance(playerTransform.position, hit.transform.position);
                if (distance <= maxDistance)
                {
                    ShowMenuAbovePlayerHead();
                }
            }
            else
            {
                HideMenu();
            }
        }
    }

    void ShowMenuAbovePlayerHead()
    {
        // Esborra el menú anterior si existeix
        if (currentMenu != null)
        {
            Destroy(currentMenu);
            currentMenu = null;
        }

        currentMenu = Instantiate(menuPrefab, uiCanvas.transform);

        // Posicionem el menú sobre el cap del jugador (2 unitats per sobre)
        Vector3 worldPosition = playerTransform.position + Vector3.up * 2f;

        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(mainCamera, worldPosition);

        RectTransform menuRect = currentMenu.GetComponent<RectTransform>();
        RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();

        // Convertim les coordenades de pantalla a locals per al canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera,
            out Vector2 localPoint
        );

        menuRect.anchoredPosition = localPoint;

        // Amaguem-lo després d'uns segons
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideAfterSeconds(menuDuration));
    }

    IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HideMenu();
    }

    void HideMenu()
    {
        if (currentMenu != null)
        {
            Destroy(currentMenu);
            currentMenu = null;
        }

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }
    }
}
