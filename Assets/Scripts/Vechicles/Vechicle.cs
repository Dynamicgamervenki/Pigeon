using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vehicle : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> pathTransformPoints = new List<Transform>();
    private int currentTargetIndex = 0;

    public bool loop = false;

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

                    // Check if we should loop back to the beginning
                    if (currentTargetIndex >= pathTransformPoints.Count)
                    {
                        if (loop)
                        {
                            currentTargetIndex = 0; // Loop back to the beginning
                        }
                        else
                        {
                            agent.isStopped = true; // Stop the agent if loop is false
                        }
                    }
                }
            }
        }
    }
}
