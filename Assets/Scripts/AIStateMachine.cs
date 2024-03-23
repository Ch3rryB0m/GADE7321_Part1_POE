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
    public bool IsPlayerVisible = false;
    public Transform player;
    public float radius;
    [Range(0f, 1f)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    // Return
    public Transform redBase;
    public bool holdingRedFlag;
 

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
        FieldOfView();
        FindObjectOfType<Score>();
        // Handle input or other conditions to determine state transitions
        if (holdingRedFlag == true)
        {
            SetState(AIState.Return);
        }
        //else if (Collider)
        //{
            
        //    SetState(AIState.Attack);
        //}
        else if (IsPlayerVisible == true)
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
                // Code for jumping state
                {
                    agent.SetDestination(redBase.position);
                    // Update is called once per frame
                    
                    
                    
                }
                break;
            case AIState.Chase:
                // Code for attacking state
                {
                    agent.SetDestination(player.position);
                    Debug.Log("I am chasing");
                   
                }
                break;
            case AIState.Attack:
                // Code for attacking state
                {
                     
                    Debug.Log("AI is attacking");
                   
                }
                break;
            case AIState.PickupBlueFlag:
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
    public void FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if(rangeChecks.Length != 0) 
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget =(target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionToTarget) < angle /2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    IsPlayerVisible = true;
                    agent.SetDestination(player.position);
                }
                else
                {
                    IsPlayerVisible = false;
                }
            }
            else if (IsPlayerVisible)
            {
                IsPlayerVisible = false;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RedFlag")
        {
            other.transform.SetParent(aiDest.transform);
            holdingRedFlag = true;
            Debug.Log("AI Picked up red flag");
        }
    }
}
