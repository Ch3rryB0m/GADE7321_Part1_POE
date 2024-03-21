using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlagState : State
{
    public Transform playerFlagTransform; // Reference to the player's flag transform
    public Transform aiFlagTransform; // Reference to the AI's flag transform
    public AIController aiController;
    public override State RunCurrentState()
    {
        AIController aiController = FindObjectOfType<AIController>();

        // Check if the player's flag is within pickup range
        if (aiController.FlagWithinPickupRange(playerFlagTransform))
        {
            // Implement behavior to pick up the player's flag
            aiController.PickupFlag();
            Debug.Log("AI is picking up the player's flag");
        }

        // Check if the AI's flag is within pickup range
        if (aiController.FlagWithinPickupRange(aiFlagTransform))
        {
            // Implement behavior to pick up the AI's flag
            aiController.PickupFlag();
            Debug.Log("AI is picking up its own flag");
        }

        return this; // Remain in the pickup flag state
    }
}
