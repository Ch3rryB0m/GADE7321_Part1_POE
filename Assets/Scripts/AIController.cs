using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Transform flagTransform; // Reference to the flag's transform
    public Transform aiFlagDest; // Destination for AI to pick up the flag
    public Transform playerFlagTransform; // Reference to the player's flag transform
    public Transform aiFlagTransform; // Reference to the AI's flag transform
    public float detectionRange = 10f; // Range within which the AI can detect the player
    public float attackRange = 2f; // Range within which the AI can attack the player
    public float pickupRange = 2f; // Range within which the AI can pick up the flag

    private StateManager stateManager;

    void Start()
    {
        stateManager = GetComponent<StateManager>();
        InitializeStates();
    }

    void Update()
    {
        // Update the state machine
        stateManager.Update();
    }

    private void InitializeStates()
    {
        //// Create instances of state classes
        IdleState idleState = new IdleState();
        ChaseState chaseState = new ChaseState();
        AttackState attackState = new AttackState();
        PickupFlagState pickupFlagState = new PickupFlagState();

        //// Set references and parameters for states
        //idleState.chaseState = chaseState;
        //idleState.pickupFlagState = pickupFlagState;
        //chaseState.attackState = attackState;
        //chaseState.playerTransform = playerTransform; // Pass playerTransform to ChaseState
        //attackState.playerTransform = playerTransform; // Pass playerTransform to AttackState
        //pickupFlagState.playerFlagTransform = playerFlagTransform; // Pass playerFlagTransform to PickupFlagState
        //pickupFlagState.aiFlagTransform = aiFlagTransform; // Pass aiFlagTransform to PickupFlagState

        // Set the initial state
        stateManager.currentState = idleState;
    }

    // Method to check if the player is within detection range
    public bool PlayerWithinDetectionRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= detectionRange;
    }

    // Method to check if the player is within attack range
    public bool PlayerWithinAttackRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= attackRange;
    }

    // Method to check if the flag is within pickup range
    public bool FlagWithinPickupRange(Transform playerFlagTransform)
    {
        return Vector3.Distance(transform.position, flagTransform.position) <= pickupRange;
    }

    // Method to pick up the flag
    public void PickupFlag()
    {
        Debug.Log("AI picking up flag");
        // Move the flag to AI's destination
        flagTransform.position = aiFlagDest.position;
        flagTransform.parent = aiFlagDest;
    }

    // Method to drop the flag
    public void DropFlag()
    {
        Debug.Log("AI dropping flag");
        // Remove the flag from AI's destination
        flagTransform.parent = null;
    }
}


