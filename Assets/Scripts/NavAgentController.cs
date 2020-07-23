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

    public void moveToPosition(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public bool hasReachedDestination() {
        bool hasReached = false;
         if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    hasReached = true;
                }
            }
        }
        return hasReached;
    }

}