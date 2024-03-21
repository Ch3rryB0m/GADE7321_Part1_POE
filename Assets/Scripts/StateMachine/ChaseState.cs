using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public Transform playerTransform;
    public AttackState attackState;
    public bool IsInAttackRange;
    public override State RunCurrentState()
    {
        if (IsInAttackRange)
        {
            return attackState;
        }
        else 
        {
            return this;
        }

    }
}
