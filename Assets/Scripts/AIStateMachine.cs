using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AIStateMachine : MonoBehaviour
{
    // reference to score
    public Score score;
    
    // Pickup red flag
    public NavMeshAgent agent; 
    public Transform redFlag; 
    public Transform aiDest;

    // Chase
    public bool IsInAttackRange = false;
    public Transform player;
    
    // Return
    public Transform redBase;
    public Transform redBaseSpawn;
    public bool holdingRedFlag;

    //Attack
    public bool collidedWithPlayer;
    public float Range = 10;
    public PlayerMovement playerMove;

    //Pickup blue flag
    public float flagPickupRange = 1f;
    public Transform blueFlag;

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
        
        FindObjectOfType<Score>();
        score.UpdateScoreText();
        //CheckCollisionWithPlayer();
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
        else if(blueFlag != null && Vector3.Distance(transform.position, blueFlag.position) < flagPickupRange && playerMove.holdingFlag == false)
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

                    agent.SetDestination(redFlag.position);
                    Debug.Log("AI is getting red flag");
                   
                    if (redFlag != null && Vector3.Distance(transform.position, redFlag.position) < flagPickupRange)
                    {

                        redFlag.SetParent(aiDest);
                        redFlag.localPosition = Vector3.zero;
                        redFlag.GetComponent<Rigidbody>().isKinematic = true;
                        holdingRedFlag = true;
                        Debug.Log("AI picked up red flag");
                    }

                }
                break;
            case AIState.Return:
                // Code for return state
                {
                    agent.SetDestination(redBase.position);
                    if (Vector3.Distance(transform.position, redBase.position) < 1f && holdingRedFlag)
                    {
                        RedDrop();
                    }
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
                // Code for attack state
                {
                    playerMove.DropFlag();
                    Debug.Log("AI is attacking");
                   
                }
                break;
            case AIState.PickupBlueFlag:
                // Code for blueflag pick up
                {
                    blueFlag.position = redBase.position;
                    blueFlag = null;
                    

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
   
    // Method to drop red flag when AI reaches its base
    public void RedDrop() 
    {
        if (holdingRedFlag)
        {
            
            redFlag.position = redBaseSpawn.position;
            redFlag.SetParent(null);
            redFlag.GetComponent<Rigidbody>().isKinematic = false;
            holdingRedFlag = false;
            Debug.Log("AI dropped the red flag.");

            score.IncreaseAIScore();
            score.RespawnFlags();
        }
        else
        {
            Debug.Log("No flag to drop.");
        }
        
    }
   
    //Trigger events
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
        if (other.tag == "Player" && holdingRedFlag == true)
        {
            holdingRedFlag = false;
            playerMove.ReturnFlag();
            Debug.Log("holding flag collision triggered");
        }
        if (other.tag == "RedBaseSpawn")
        {
            RedDrop();
            Debug.Log("ai dropped the flag and scored.");
        }
    }
}
