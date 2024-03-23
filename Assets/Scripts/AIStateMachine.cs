using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    // Define your player states here
    public enum AIState
    {
        Idle,
        Chase,
        Attacking,
        Pickup
        // Add more states as needed
    }
   
    public Transform playerFlagTransform; // Reference to the player's flag transform
    public Transform aiFlagTransform; // Reference to the AI's flag transform
    public AIController aiController;
    public Transform playerTransform; 
    public bool IsInAttackRange;
    // Current state of the player
    private AIState currentState;

    void Start()
    {
        // Set the initial state when the script starts
        SetState(AIState.Idle);
    }

    void Update()
    {
        // Handle input or other conditions to determine state transitions
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetState(AIState.Chase);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SetState(AIState.Attacking);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            SetState(AIState.Pickup);
        }
        else
        {
            SetState(AIState.Idle);
        }

        // Perform state-specific behavior
        PerformStateBehavior();
    }

    // Method to set the current state
    private void SetState(AIState newState)
    {
        currentState = newState;
        // Perform any additional actions when entering a new state
        EnterState();
    }

    // Method to handle state-specific behavior
    private void PerformStateBehavior()
    {
        // Implement behavior specific to each state
        switch (currentState)
        {
            case AIState.Idle:
                // Code for idle state
                {
                    // Implement idle state behavior
                    Debug.Log("AI is idle");

                    AIController aiController = FindObjectOfType<AIController>();

                    // Check if the player is within detection range and transition to chase state if true
                    if (aiController.PlayerWithinAttackRange())
                    {
                        currentState = AIState.Chase;
                    }


                    // Check if the AI's flag is within pickup range and transition to pickup flag state if true
                    if (aiController.FlagWithinPickupRange(aiController.DefendFlag))
                    {
                        currentState = AIState.Pickup;
                    }

                    // Otherwise, remain in idle state
                    currentState = AIState.Idle;
                }
                break;
            case AIState.Chase:
                // Code for jumping state
                {

                    if (IsInAttackRange)
                    {
                        currentState = AIState.Attacking;
                    }
                    else
                    {
                        currentState = AIState.Chase;
                    }
                }
                break;
            case AIState.Attacking:
                // Code for attacking state
                {
                    Debug.Log("I have attacked");
                    currentState = AIState.Attacking;
                }
                break;
            case AIState.Pickup:
                // Code for attacking state
                {
                    // Check if the player's flag is within pickup range
                    if (aiController.FlagWithinPickupRange(playerFlagTransform))
                    {
                        // Implement behavior to pick up the player's flag
                        aiController.PickupFlag(playerFlagTransform);
                        Debug.Log("AI is picking up the player's flag");
                    }

                    // Check if the AI's flag is within pickup range
                    if (aiController.FlagWithinPickupRange(aiFlagTransform))
                    {
                        // Implement behavior to pick up the AI's flag
                        aiController.PickupFlag(aiFlagTransform);
                        Debug.Log("AI is picking up its own flag");
                    }

                    currentState = AIState.Pickup; // Remain in the pickup flag state
                }
                break;
        }
    }

    private void EnterState()
    {
        // Implement any actions needed when entering a new state
        Debug.Log("Entering State: " + currentState);
    }
}
