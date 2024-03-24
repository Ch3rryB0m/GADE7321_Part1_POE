using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AIStateMachine : MonoBehaviour
{
    // Pickup red
    public NavMeshAgent agent; 
    public Transform redFlag; 
    public Transform aiDest;

    // Chase
    public bool IsInAttackRange = false;
    public Transform player;
    
    // Return
    public Transform redBase;
    public bool holdingRedFlag;

    //Attack
    public bool collidedWithPlayer;
    public float Range = 10;
    public PlayerMovement playerMove;

    // ai states 
    public enum AIState
    {
        PickupRedFlag,
        Return,
        Chase,
        Attack,
        PickupBlueFlag
    }

    

    // Current state of the player
    private AIState currentState;

    void Start()
    {
        // Set the initial state when the script starts
        SetState(AIState.PickupRedFlag);
    }

    void Update()
    {
        //FieldOfView(); 
        float Distance;
        Distance = Vector3.Distance(this.transform.position, player.transform.position);
        // Handle input or other conditions to determine state transitions
        if (holdingRedFlag == true)
        {
            SetState(AIState.Return);
        }
        else if (collidedWithPlayer == true)
        {
            SetState(AIState.Attack);
        }
        else if (Distance <= Range && playerMove.holdingFlag == true )
        {
            SetState(AIState.Chase);
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            SetState(AIState.PickupBlueFlag);
        }
        else
        {
            SetState(AIState.PickupRedFlag);
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
            case AIState.PickupRedFlag:
                // Code for red pickup state
                {
                    // Implement idle state behavior
                    agent.SetDestination(redFlag.position);
                    Debug.Log("AI is getting red flag");

                }
                break;
            case AIState.Return:
                // Code for return state
                {
                    agent.SetDestination(redBase.position);
                    // Update is called once per frame
                    
                }
                break;
            case AIState.Chase:
                // Code for chase state
                {
                    agent.SetDestination(player.position);
                    Debug.Log("I am chasing");
                   
                }
                break;
            case AIState.Attack:
                // Code for attacking state
                {
                    playerMove.DropFlag();
                    Debug.Log("AI is attacking");
                   
                }
                break;
            case AIState.PickupBlueFlag:
                // Code for blueflag pick up
                {
                    Debug.Log("Ai is getting blue flag");
                }
                break;
        }
    }

    private void EnterState()
    {
        // Implement any actions needed when entering a new state
        Debug.Log("Entering State: " + currentState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RedFlag")
        {
            other.transform.SetParent(aiDest.transform);
            holdingRedFlag = true;
            Debug.Log("AI Picked up red flag");
        }
        if (other.tag == "Player")
        {
            playerMove.DropFlag();
            SetState(AIState.Attack);
            
        }
    }
}
