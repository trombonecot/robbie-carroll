using UnityEngine;

public class ContainerUI : Inventory
{
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

    public void OpenPanel()
    {
        containerPanel.SetActive(true);
        panelScript.ShowItems(this);
    }

    public void ClosePanel()
    {
        containerPanel.SetActive(false);
    }
}
