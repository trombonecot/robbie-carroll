using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public Item[] items = new Item[4];
    public GameObject containerPanel;
    public float interactionDistance = 3f;

    private Transform player;
    private ContainerPanelUI panelScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        panelScript = containerPanel.GetComponent<ContainerPanelUI>();
        containerPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        TogglePanel();
        if (Vector3.Distance(transform.position, player.position) <= interactionDistance)
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        bool isActive = containerPanel.activeSelf;
        containerPanel.SetActive(!isActive);

        if (!isActive)
        {
            panelScript.ShowItems(items);
        }
    }
}
