using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    public GameObject player;
    public float interactionDistance = 2f;
    public float dialogDuration = 3f;
    public GameObject hoverCanvas;

    private GameObject dialogInstance;
    private float dialogTimer;

    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
        if (hoverCanvas != null)
        {
            hoverCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance <= interactionDistance)
                {
                    player.GetComponent<Animator>().SetTrigger("open");
                }
            }
        }
    }
}
