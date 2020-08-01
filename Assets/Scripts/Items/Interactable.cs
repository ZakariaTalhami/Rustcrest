using UnityEngine;

[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{

    private NavAgentController playerAgent;
    private bool hasInteracted = false;
    public float interactionDistance = 3f;


    public virtual void SetInteraction(NavAgentController playerAgent)
    {
        Debug.Log("started intraction!");
        hasInteracted = false;
        this.playerAgent = playerAgent;
        SetAgentMovement(GetInteractablePosition());
    }

    public void EndInteraction()
    {
        playerAgent = null;
        hasInteracted = false;
    }

    private void SetAgentMovement(Vector3 pos)
    {
        if (playerAgent)
        {
            playerAgent.MoveToPosition(pos, interactionDistance);
        }
        else
        {
            Debug.LogError("Failed to set Destination: PlayerAgent not set!");
        }
    }

    public virtual void Interacte()
    {
        Debug.Log("Interacted with an itractable (Duh!)");
    }

    private void Update()
    {
        if (!hasInteracted && playerAgent != null && !playerAgent.IsPathPending())
        {
            SetAgentMovement(GetInteractablePosition());
            if (playerAgent.HasReachedDestination())
            {
                Interacte();
                hasInteracted = true;
            }
        }
    }

    private Vector3 GetInteractablePosition()
    {
        return transform.position;
    }

}