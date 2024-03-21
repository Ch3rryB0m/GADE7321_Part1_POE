using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour  where EState : Enum
{
//    protected Dictionary<EState , State<EState>> States = new Dictionary<EState, State<EState>> ();

//    protected State<EState> CurrentState;

//    protected bool IsTransitioningState = false;

//    void Start()
//    {
//        CurrentState.EnterState();
//    }

//    void Update()
//    {
//        EState nextStateKey =CurrentState.GetNextState();
//        if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey)) 
//        {
//            CurrentState.UpdateState();
//        }
//        else if(!IsTransitioningState) 
//        {
//            TransitionToState(nextStateKey);
//        }
//        CurrentState.UpdateState();
//    }
//    public void TransitionToState(EState statekey)
//    {
//        IsTransitioningState = true;
//        CurrentState.ExitState();
//        CurrentState = States[statekey];
//        CurrentState.EnterState();
//        IsTransitioningState = false;
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        CurrentState.OnTriggerEnter(other);
//    }

//    void OnTriggerStay(Collider other)
//    {
//        CurrentState.OnTriggerStay(other);
//    }
//    void OnTriggerExit(Collider other)
//    {
//        CurrentState.OnTriggerExit(other);
//    }
}
