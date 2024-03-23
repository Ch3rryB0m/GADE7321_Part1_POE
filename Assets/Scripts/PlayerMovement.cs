using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public Transform blueBaseTransform; // Reference to the blue base's transform
    public Transform redBaseTransform; // Reference to the red base's transform

    public bool holdingFlag = false; // Flag to track if the player is holding the flag
    private Transform currentFlag; // Reference to the flag currently held by the player
    public AIController aicontroller;
    public float speed = 5f; // Movement speed
    public float staggerDuration = 0.5f; // Duration of stagger when hit

    private bool canMove = true; // Flag to control player movement after stagger

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canMove)
        {
            // Movement input
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * speed);

            // Check for flag toggle pickup/drop
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!holdingFlag) // If not holding a flag, try to pick up
                {
                    TryPickupFlag();
                }
                else // If holding a flag, drop it
                {
                    DropFlag();
                }
            }

            // Check for flag capture
            if (holdingFlag && currentFlag != null)
            {
                if (Vector3.Distance(transform.position, blueBaseTransform.position) < 1f) // Assuming player's base is blue
                {
                    // Return the flag to the base and score a point
                    ReturnFlag();
                }
            }
        }
    }

    // Method to try to pick up the flag
    private void TryPickupFlag()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("flag"))
            {
                // Pick up the flag
                currentFlag = hit.transform;
                holdingFlag = true;
                Debug.Log("Player picked up the flag.");
            }
        }
    }

    // Method to drop the flag
    public void DropFlag()
    {
        // Drop the flag
        currentFlag.position = transform.position + transform.forward * 2f; // Drop in front of the player
        currentFlag = null;
        holdingFlag = false;
        Debug.Log("Player dropped the flag.");
    }

    // Method to return the flag to the base and score a point
    private void ReturnFlag()
    {
        // Return the flag to the base and score a point
        currentFlag.position = blueBaseTransform.position;
        currentFlag = null;
        holdingFlag = false;
        Debug.Log("Player scored a point.");
    }

    // Method to handle collision with opponent carrying the flag
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI") && other.GetComponent<AIController>().IsHoldingFlag)
        {
            // Force the opponent to drop the flag
            other.GetComponent<AIController>().DropFlag();

            // Stagger the player for a short duration
            StartCoroutine(Stagger());
        }
    }

    // Coroutine for player stagger
    private IEnumerator Stagger()
    {
        canMove = false;
        yield return new WaitForSeconds(staggerDuration);
        canMove = true;
    }
} 
