using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public  class StateManager : MonoBehaviour 
{
    public State currentState;

    public void Update()
    {
        RunStateMachine();
    }

    public void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null && nextState != currentState)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }
}

