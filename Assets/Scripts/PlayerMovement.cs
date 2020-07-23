using UnityEngine;

[RequireComponent(typeof(NavAgentController))]
public class PlayerMovement : MonoBehaviour
{
    public GameObject pfbMovementMarker;
    public LayerMask walkableLayers;
    private NavAgentController agentController;
    private Vector3 targetPosition;

    // MovementMaker
    private GameObject goMovementMarker;

    private void Start()
    {
        agentController = GetComponent<NavAgentController>();
    }

    void Update()
    {
        // Check for movement inputs.
        if (Input.GetMouseButton(1))
        {
            targetPosition = MouseUtil.getMousePositionInWorld(walkableLayers);
            Debug.Log(targetPosition);
            setMovementMarker(targetPosition);
            agentController.moveToPosition(targetPosition);
            // Stop focusing on an Item.
        }

        // Check for Interaction actions.
        if (Input.GetMouseButton(0))
        {
            // Set Focus on an interactable.
        }

        if (agentController.hasReachedDestination())
        {
            removeMovementMarker();
        }

    }

    private void setMovementMarker(Vector3 pos)
    {
        removeMovementMarker();
        goMovementMarker = Instantiate(pfbMovementMarker, pos, Quaternion.identity);
    }

    private void removeMovementMarker()
    {
        if (goMovementMarker) Destroy(goMovementMarker);
    }

}
