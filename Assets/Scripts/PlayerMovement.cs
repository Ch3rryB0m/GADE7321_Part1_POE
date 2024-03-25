using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    public Score score;
    private CharacterController controller;

    public Transform blueBaseTransform; // Reference to the blue base's transform
    public Transform redBaseTransform; // Reference to the red base's transform

    public bool holdingFlag = false; // Flag to track if the player is holding the flag
    public Transform blueFlag; // Reference to the flag currently held by the player
    public Transform redFlag; // Reference to the flag held by the AI

    public Transform playerDest; // Destination transform for holding the flag

    public float speed = 5f; // Movement speed
    public float staggerDuration = 0.5f; // Duration of stagger when hit
    public float flagPickupRange = 1f; // Range for flag pickup

    private bool canMove = true; // Flag to control player movement after stagger

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        FindObjectOfType<Score>();
        score.UpdateScoreText();
        if (canMove)
        {
            // Movement input
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * speed);

            // Update flag position if holding
            if (holdingFlag)
            {
                blueFlag.position = playerDest.position;
            }

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
            if (holdingFlag && redFlag != null)
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
        if (blueFlag != null && Vector3.Distance(transform.position, blueFlag.position) < flagPickupRange)
        {
            // Pick up the flag
            blueFlag.SetParent(playerDest);
            holdingFlag = true;
            Debug.Log("Player picked up the flag.");
        }
        else if (!holdingFlag && redFlag != null && Vector3.Distance(transform.position, redFlag.position) < flagPickupRange)
        {
            ReturnFlag();
        }
        else
        {
            Debug.Log("Flag is out of range for pickup.");
        }
    }

    // Method to drop the flag
    public void DropFlag()
    {
        if (holdingFlag)
        {
            // Drop the flag
            blueFlag.parent = null;
            holdingFlag = false;
            Debug.Log("Player dropped the flag.");
        }
        else
        {
            Debug.Log("Player has no flag to drop.");
        }
    }

    // Method to return the red flag to the base 
    private void ReturnFlag()
    {
        if (redFlag != null && Vector3.Distance(transform.position, redFlag.position) < flagPickupRange)
        {
            // Return the red flag to the base
            redFlag.position = blueBaseTransform.position;
            redFlag = null;
            holdingFlag = false;
            Debug.Log("Player recaptured red Flag");
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered: " + other.tag);

        if (other.CompareTag("BlueFlag") && blueFlag.position == blueBaseTransform.position)
        {
            Debug.Log("Player triggered blue flag.");
            score.IncreasePlayerScore();
            score.RespawnFlags();
        }

    }
} 
