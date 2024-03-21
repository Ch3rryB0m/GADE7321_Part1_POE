using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State 
{
    public ChaseState chaseState; // Reference to the chase state
    public PickupFlagState pickupFlagState; // Reference to the pickup flag state

    public override State RunCurrentState()
    {
        // Implement idle state behavior
        Debug.Log("AI is idle");

        AIController aiController = FindObjectOfType<AIController>();

        // Check if the player is within detection range and transition to chase state if true
        if (chaseState != null && aiController.PlayerWithinAttackRange())
        {
            return chaseState;
        }

        
        // Check if the AI's flag is within pickup range and transition to pickup flag state if true
        if (pickupFlagState != null && aiController.FlagWithinPickupRange(aiController.aiFlagTransform))
        {
            return pickupFlagState;
        }

        // Otherwise, remain in idle state
        return this;
    }

}
