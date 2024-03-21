using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public Transform playerTransform;
    public override State RunCurrentState()
    {
        Debug.Log("I have attacked");
        return this;
    }
}
