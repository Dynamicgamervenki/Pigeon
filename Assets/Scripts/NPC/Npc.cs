using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private Animator anim;

    public enum NpcState
    {
        Running,
        Walking,
        Jogging,
        sitting_Clapping,
        sitting_Leg,
        sitting_Laughing
    }

    public NpcState currentState;

    public float runningSpeed = 10.0f;
    public float walkingSpeed = 3.0f;
    public float joggingSpeed = 5.0f;

    private void Awake()
    {
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
        switch (currentState)
        {
            case NpcState.Running:
                transform.position += transform.forward * runningSpeed * Time.deltaTime;
                break;
            case NpcState.Walking:
                transform.position += transform.forward * walkingSpeed * Time.deltaTime;
                break;
            case NpcState.Jogging:
                transform.position += transform.forward * joggingSpeed * Time.deltaTime;
                break;
            case NpcState.sitting_Clapping:
                // Sitting typically doesn't involve movement, so we can skip changing the position.
                break;
            case NpcState.sitting_Leg:
                break;
            case NpcState.sitting_Laughing:
                break;
        }
    }

    private void SetAnimationState(NpcState state)
    {
        // Reset all animation states
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isJogging", false);
        anim.SetBool("isSitting", false);

        switch (state)
        {
            case NpcState.Running:
                anim.SetBool("isRunning", true);
                break;
            case NpcState.Walking:
                anim.SetBool("isWalking", true);
                break;
            case NpcState.Jogging:
                anim.SetBool("isJogging", true);
                break;
            case NpcState.sitting_Clapping:
                anim.SetBool("isSitting_Clap", true);
                break;
            case NpcState.sitting_Leg:
                anim.SetBool("isSitting_Leg", true);
                break;
            case NpcState.sitting_Laughing:
                anim.SetBool("isSitting_Laughing", true);
                break;
        }
    }
}
