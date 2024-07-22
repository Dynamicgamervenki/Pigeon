using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> pathTransformPoints = new List<Transform>();
    private int currentTargetIndex = 0;
    private Animator anim;

    public enum NpcState
    {
        Idle,
        Running,
        Walking,
        Jogging,
        sitting_Clapping,
        sitting_Leg,
        sitting_Laughing,
        sitting_Talking,
        sitting_Gun,
        standing_Arguing,
        Dancing,
        Cheering,
        Drunk,
        Dance,
        Talking,
        Excited,
    }

    public NpcState currentState;

    public float runningSpeed = 10.0f;
    public float walkingSpeed = 3.0f;
    public float joggingSpeed = 5.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SetAnimationState(currentState);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Check if the agent and path points are valid
        if (agent != null && pathTransformPoints.Count > 0)
        {
            // Check if the current target index is within bounds
            if (currentTargetIndex < pathTransformPoints.Count)
            {
                // Set the destination to the current target point
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
                anim.Play("Breathing Idle", 0);
            }
        }

        // Update animation based on movement
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        // Determine movement state based on NavMeshAgent velocity
        float speed = agent.velocity.magnitude;
        bool isMoving = speed > 0.1f;

        // Set animation states based on NPC's current state and movement
        switch (currentState)
        {
            case NpcState.Idle:
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isSitting_Laughing", false);
                anim.SetBool("isSitting_Leg", false);
                // Handle other sitting animations if needed
                break;
            case NpcState.Walking:
                anim.SetBool("isWalking", isMoving);
                anim.SetBool("isRunning", false);
                anim.SetBool("isSitting_Laughing", false);
                anim.SetBool("isSitting_Leg", false);
                // Handle other walking animations if needed
                break;
            case NpcState.Running:
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", isMoving);
                anim.SetBool("isSitting_Laughing", false);
                anim.SetBool("isSitting_Leg", false);
                // Handle other running animations if needed
                break;
            case NpcState.sitting_Laughing:
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isSitting_Laughing", true);
                anim.SetBool("isSitting_Leg", false);
                // Handle other sitting laughing animations if needed
                break;
            case NpcState.sitting_Leg:
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isSitting_Laughing", false);
                anim.SetBool("isSitting_Leg", true);
                // Handle other sitting leg animations if needed
                break;
            // Handle other states as needed
            default:
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isSitting_Laughing", false);
                anim.SetBool("isSitting_Leg", false);
                // Handle other animations if needed
                break;
        }
    }

    private void SetAnimationState(NpcState state)
    {
        // Reset all animation states
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isSitting_Laughing", false);
        anim.SetBool("isSitting_Leg", false);
        // Add more resets for other animations if needed

        // Set animation state based on the given state
        switch (state)
        {
            case NpcState.Idle:
                anim.Play("Breathing Idle", 0);
                break;
            case NpcState.Running:
                anim.SetBool("isRunning", true);
                break;
            case NpcState.Jogging:
                anim.SetBool("isJogging", true);
                break;
            case NpcState.Walking:
                anim.SetBool("isWalking", true);
                break;
            case NpcState.sitting_Laughing:
                anim.SetBool("isSitting_Laughing", true);
                break;
            case NpcState.sitting_Leg:
                anim.SetBool("isSitting_Leg", true);
                break;
            case NpcState.sitting_Clapping:
                anim.SetBool("isSitting_Clap", true);
                break;
            case NpcState.sitting_Talking:
                anim.SetBool("isSitting_Talking", true);
                break;
            case NpcState.sitting_Gun:
                anim.SetBool("isSitting_GunMotion", true);
                break;
            case NpcState.standing_Arguing:
                anim.SetBool("isStanding_Arguing", true);
                break;
            case NpcState.Dancing:
                anim.SetBool("isDancing", true);
                break;
            case NpcState.Cheering:
                anim.SetBool("isCheering", true);
                break;
            case NpcState.Drunk:
                anim.SetBool("isDrunk", true);
                break;
            case NpcState.Dance:
                anim.SetBool("isDance", true);
                break;
            case NpcState.Talking:
                anim.SetBool("isTalking", true);
                break;
            case NpcState.Excited:
                anim.SetBool("isExcited", true);
                break;
            // Add cases for other states
            default:
                break;
        }
    }
}
