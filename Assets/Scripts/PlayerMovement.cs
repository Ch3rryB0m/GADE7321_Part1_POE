using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    public Score score;
    private CharacterController controller;
    AIStateMachine stateMachine;

    public Transform blueBaseTransform; //  blue base's transform
    public Transform blueBaseFlagSpawn; // where the flag respawns at the blue base
    public Transform redBaseTransform; // red base's transform

    public bool holdingFlag = false; // track if the player is holding a flag
    public Transform blueFlag; // blue flag transform
    public Transform redFlag; // red flag transform

    public Transform player; // players transform
    public Transform playerDest; // Destination transform for holding the flag

    public float speed = 5f; // Movement speed
    public float flagPickupRange = 1f; // Range for flag pickup

    private bool canMove = true;// don't need this anymore but too scared to change it

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
            // F to pickup flag
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!holdingFlag) // If not holding a flag, try to pick up
                {
                    TryPickupFlag();
                }
                
            }
            // Checks for flag capture
            if (holdingFlag && redFlag != null)
            {
                if (Vector3.Distance(transform.position, blueBaseTransform.position) < 1f) 
                {
                    // Return the flag to the blue base 
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

            blueFlag.position = blueBaseTransform.position;
            blueFlag.SetParent(null);
            blueFlag.GetComponent<Rigidbody>().isKinematic = false;
            holdingFlag = false;
            Debug.Log("player dropped the red flag.");

            score.IncreasePlayerScore();
            score.RespawnFlags();
        }
        else
        {
            Debug.Log("No flag to drop.");
        }
    }

    // Method to return the red flag to the blue base 
    public void ReturnFlag()
    {
        if (redFlag != null)
        {
            redFlag.position = blueBaseTransform.position;
            redFlag.SetParent(blueBaseFlagSpawn);
            Debug.Log("Player recaptured red Flag");
        }
    }
    // not sure if this is necessary anymore but not sure what removing it will do
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered: " + other.tag);

        if (other.CompareTag("BlueBaseSpawn") && holdingFlag == true)
        {
            Debug.Log("point works blue base collision");
            DropFlag();
        }
        

    }
} 
