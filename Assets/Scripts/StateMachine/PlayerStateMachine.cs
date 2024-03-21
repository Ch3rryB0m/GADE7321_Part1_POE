using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // Define player states
    public enum PlayerState
    {
        Idle,
        Walking,
        Attacking
        // Add more states as needed
    }

    // Current state of the player
    private State currentState;

    // Reference to the AI controller
    private AIController aiController;

    void Start()
    {
        // Set the initial state when the script starts
        SetState(new IdleState());

        // Get a reference to the AI controller
        aiController = FindObjectOfType<AIController>();
    }

    void Update()
    {
        // Update the state machine
        currentState = currentState.RunCurrentState();

        // Check for interactions with the AI
        CheckAIInteractions();
    }

    // Method to set the current state
    private void SetState(State newState)
    {
        currentState = newState;
    }

    // Method to check for interactions with the AI
    private void CheckAIInteractions()
    {
        // Example: If the player is attacking and the AI is within attack range, trigger attack
        if (currentState is AttackingState && aiController != null && aiController.PlayerWithinAttackRange())
        {
            Debug.Log("Player is attacking and AI is within attack range. Trigger attack!");
        }
    }
    private class AttackingState : State
    {
        public override State RunCurrentState()
        {
            // Implement attacking state behavior
            Debug.Log("Player is attacking");
            return this;
        }
    }
}


