using UnityEngine;

public class InventoryContainer : Inventory
{
    public GameObject containerPanel;
    public float interactionDistance = 3f;
    public bool openOnLoad = false;

    private Transform player;
    private InventoryUI panelScript;

    void Start()
    {
        panelScript = containerPanel.GetComponent<InventoryUI>();
       
        if (openOnLoad)
        {
            OpenPanel();
        } else
        {
            containerPanel.SetActive(false);
        }
    }

    public void OpenPanel()
    {
        containerPanel.SetActive(true);
        panelScript.Connect(this);
    }

    public void ClosePanel()
    {
        containerPanel.SetActive(false);
        panelScript.Disconnect();
    }
}
