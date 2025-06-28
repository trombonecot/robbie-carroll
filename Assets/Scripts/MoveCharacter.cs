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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(interactableTag))
                {
                    float distance = Vector3.Distance(transform.position, hit.point);
                    if (distance <= interactionDistance)
                    {
                        animator.SetTrigger("open");
                        ContainerUI containerUi = hit.collider.GetComponent<ContainerUI>();

                        if (containerUi != null)
                        {
                            containerUi.OpenPanel();
                        }


                        return;
                    }
                }

                containerPanel.SetActive(false);

                MoveToPosition(hit.point);
            }
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }

    void MoveToPosition(Vector3 position)
    {
        if (agent != null)
        {
            agent.SetDestination(position);
            animator.SetBool("isWalking", true);
        }
    }
}
