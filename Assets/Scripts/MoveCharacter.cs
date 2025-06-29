using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MoveCharacter : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;
    public float interactionDistance = 2f;
    public string interactableTag = "Interactable";
    public GameObject containerPanel;

    private Transform currentTarget;
    private InventoryContainer currentInventory;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag(interactableTag))
                {
                    currentTarget = hit.collider.transform;
                    currentInventory = hit.collider.GetComponent<InventoryContainer>();

                    MoveToPosition(currentTarget.position);
                    return;
                }

                containerPanel.SetActive(false);
                currentTarget = null;
                currentInventory = null;
                MoveToPosition(hit.point);
            }
        }

        animator.SetBool("isWalking", !agent.pathPending && agent.remainingDistance > agent.stoppingDistance);

        if (currentTarget != null && !agent.pathPending && agent.remainingDistance <= interactionDistance)
        {
            animator.SetTrigger("open");

            if (currentInventory != null)
            {
                currentInventory.OpenPanel();
            }

            currentTarget = null;
            currentInventory = null;
        }
    }

    void MoveToPosition(Vector3 position)
    {
        if (agent != null)
        {
            agent.SetDestination(position);
        }
    }
}
