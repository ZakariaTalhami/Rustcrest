using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavAgentController))]
public class PlayerMovement : MonoBehaviour
{

    public LayerMask walkableLayers;
    private NavAgentController agentController;
    private Vector3 targetPosition;

    // MovementMaker
    public GameObject pfbMovementMarker;
    private GameObject goMovementMarker;
    private Interactable interactedObject;
    private void Start()
    {
        agentController = GetComponent<NavAgentController>();
    }

    void Update()
    {
        // Check for movement inputs.
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            MouseClickEvent mouseClick = MouseUtil.GetMousePositionInWorld();
            targetPosition = mouseClick.point;
            SetMovementMarker(targetPosition);

            if (mouseClick.hitGameObject.tag == "Intractable")
            {
                // End old interaction 
                if(interactedObject != null)
                {
                    interactedObject.EndInteraction();
                }

                // Interact with object
                interactedObject = mouseClick.hitGameObject.gameObject.GetComponent<Interactable>();
                interactedObject.SetInteraction(agentController);
            }
            else
            {
                agentController.MoveToPosition(targetPosition);
                if (interactedObject)
                {
                    // Stop focusing on an Item.
                    interactedObject.EndInteraction();
                }
            }
            UIEventHandler.ContainerClosed();
            UIEventHandler.DialogInterrupted();
        }

        if (agentController.HasReachedDestination())
        {
            RemoveMovementMarker();
        }
        else
        {
            SetMovementMarker(agentController.GetAgentDestination());
        }

    }

    private void SetMovementMarker(Vector3 pos)
    {
        RemoveMovementMarker();
        goMovementMarker = Instantiate(pfbMovementMarker, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
    }

    private void RemoveMovementMarker()
    {
        if (goMovementMarker) Destroy(goMovementMarker);
    }

}
