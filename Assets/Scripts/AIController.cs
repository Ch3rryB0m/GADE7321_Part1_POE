using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform blueBaseTransform; // Reference to the blue base's transform
    public Transform redBaseTransform; // Reference to the red base's transform
    public Transform playerFlagTransform; // Reference to the player's flag transform
    public Transform aiFlagTransform; // Reference to the AI's flag transform

    public NavMeshAgent agent; // Reference to the NavMeshAgent component

    // Boolean to track if the AI is holding the flag
    private bool holdingFlag = false;

    // Boolean to track if the AI is at the base to score a point
    private bool atBase = false;

    // Boolean to track if the AI is staggered
    private bool isStaggered = false;

    // Duration of stagger when hit
    public float staggerDuration = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Set the initial destination to fetch the flag from the player's base
        agent.SetDestination(aiFlagTransform.position);
    }

    // Method to force the AI to drop the flag
    public void DropFlag()
    {
        if (holdingFlag)
        {
            holdingFlag = false;
            // Logic to drop the flag at the current position (not implemented here)
            Debug.Log("AI dropped its own flag.");
        }
    }

    // Method to handle collision with opponent carrying the flag
    private void OnTriggerEnter(Collider other)
    {
        if (!isStaggered && other.CompareTag("Player") && other.GetComponent<PlayerMovement>().holdingFlag)
        {
            // Force the AI to drop the flag and stagger
            DropFlag();
            StartCoroutine(Stagger());
        }
    }

    // Coroutine for AI stagger
    private IEnumerator Stagger()
    {
        isStaggered = true;
        yield return new WaitForSeconds(staggerDuration);
        isStaggered = false;
    }

    // Method to check if a flag is within pickup range
    public bool FlagWithinPickupRange(Transform flagTransform)
    {
        float distanceToFlag = Vector3.Distance(transform.position, flagTransform.position);
        return distanceToFlag <= 2f;
    }

    
    // Method to pick up the flag
    public void PickupFlag(Transform aiflagTransform)
    {
        
        if (!holdingFlag )
        {
            
            // Set holdingFlag to true to indicate that the AI is now holding the flag
            holdingFlag = true;

            // Set the destination of the AI to the red base
            agent.SetDestination(redBaseTransform.position);

            // Adjust the flag's parent to the AI to make it follow the AI
            aiflagTransform.SetParent(transform);

            // Disable the flag's collider so it doesn't interfere with navigation
            aiflagTransform.GetComponent<Collider>().enabled = false;

            // Log a message to indicate that the AI has picked up the flag
            Debug.Log("AI picked up the flag.");
        }
    }



    // Method to check if the player is within attack range
    public bool PlayerWithinAttackRange()
    {
        // Check if the player exists
        if (playerFlagTransform != null)
        {
            // Calculate the distance between the AI and the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerFlagTransform.position);

            // Define the attack range threshold
            float attackRange = 5f; // Adjust this value as needed

            // Return true if the player is within the attack range
            return distanceToPlayer <= attackRange;
        }
        else
        {
            // If player is not found, return false
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isStaggered)
        {
            // Check if the AI has reached the player's base
            if (!holdingFlag && Vector3.Distance(transform.position, blueBaseTransform.position) < 1f)
            {
                // AI has reached the player's base, pick up the flag
                Debug.Log("Reached the flag position. Trying to pick up the flag.");
                PickupFlag(playerFlagTransform);
            }

            // Check if the AI has reached the red base
            if (holdingFlag && Vector3.Distance(transform.position, redBaseTransform.position) < 1f)
            {
                // AI has reached the red base, drop the flag and score a point
                holdingFlag = false;
                atBase = true;
                Debug.Log("AI dropped the blue flag at the red base and scored a point.");
            }

            // Check if the AI has left the base after scoring a point
            if (atBase && Vector3.Distance(transform.position, redBaseTransform.position) > 1f)
            {
                // AI has left the base, reset the atBase flag
                atBase = false;
            }
        }
    }


}


