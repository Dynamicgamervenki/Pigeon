using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vehicle : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> pathTransformPoints = new List<Transform>();
    private int currentTargetIndex = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent != null && pathTransformPoints.Count > 0)
        {
            if (currentTargetIndex < pathTransformPoints.Count)
            {
                agent.SetDestination(pathTransformPoints[currentTargetIndex].position);

                // Check if the agent has reached the current target
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    // Move to the next target in the list
                    currentTargetIndex++;
                }
            }
            else
            {
                // Stop the agent when the last waypoint is reached
                agent.isStopped = true;
            }
        }
    }
}

