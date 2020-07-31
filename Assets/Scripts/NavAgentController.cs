using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentController : MonoBehaviour
{

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        MoveToPosition(targetPosition, 0);
    }

    public void MoveToPosition(Vector3 targetPosition, float stoppingDistance)
    {
        agent.SetDestination(targetPosition);
        agent.stoppingDistance = stoppingDistance;
    }

    public bool HasReachedDestination()
    {
        bool hasReached = false;
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                hasReached = true;
            }
        }
        return hasReached;
    }

    public bool IsPathPending()
    {
        return agent.pathPending;
    }

    public Vector3 GetAgentDestination()
    {
        return agent.destination;
    }

}