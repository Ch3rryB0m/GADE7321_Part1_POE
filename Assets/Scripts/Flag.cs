using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Blue,
    Red
}
public class Flag : MonoBehaviour
{

    public Transform playerDest; // Destination for the player to pick up the flag
    public Transform AIDest; // Destination for the AI to pick up the flag
    private bool playerInRange = false;
    private bool aiInRange = false;
    public bool holdingFlag = false;
    public Team team; // Team assigned to the flag

    void Update()
    {
        // Player interaction
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (holdingFlag)
            {
                Debug.Log("F key pressed - dropping flag");
                Drop();
            }
            else if (playerInRange)
            {
                Debug.Log("F key pressed - picking up flag");
                Pickup(playerDest);
            }
        }

        // AI interaction
        if (aiInRange && !holdingFlag)
        {
            Debug.Log("AI picking up flag");
            Pickup(AIDest);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered range");
            playerInRange = true;
        }
        else if (other.CompareTag("AI"))
        {
            Debug.Log("AI entered range");
            aiInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited range");
            playerInRange = false;
        }
        else if (other.CompareTag("AI"))
        {
            Debug.Log("AI exited range");
            aiInRange = false;
        }
    }

    public void Pickup(Transform destination)
    {
        Debug.Log("Picking up flag");
        // Disable the collider and gravity
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;

        // Move the object to the destination position
        transform.position = destination.position;

        // Set the object's parent to the destination
        transform.parent = destination;

        // Disable any movement or rotation
        GetComponent<Rigidbody>().isKinematic = true;

        holdingFlag = true;
    }

    public void Drop()
    {
        Debug.Log("Dropping flag");
        // Remove the parent
        transform.parent = null;

        // Enable gravity and collider
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;

        // Enable movement and rotation
        GetComponent<Rigidbody>().isKinematic = false;

        holdingFlag = false;
    }

    // Method to reset the flag position to the spawn point
    public void ResetFlag(bool isBlueTeam)
    {
        // Reset holding flag status
        holdingFlag = false;

        // Determine spawn point based on team color
        Transform spawnPoint = isBlueTeam ? GameObject.FindWithTag("BlueBaseSpawn").transform : GameObject.FindWithTag("RedBaseSpawn").transform;

        // Reset flag position to the appropriate spawn point
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
} 
