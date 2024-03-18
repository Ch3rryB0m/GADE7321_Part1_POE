using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState> 
{
    public enum PlayerState
    {
        Idle,
        Walk,
        PickUp,
        Steal
        
    }
    private void Awake()
    {
        CurrentState = States[PlayerState.Idle];
    }
}
