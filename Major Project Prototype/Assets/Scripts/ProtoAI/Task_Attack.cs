using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Task_Attack : BT_Node
{
    private Transform _lastTarget;

    private AI_Health _aI_Health;

    private float attackTime = 1f;
    private float attackCounter = 0f;

    public Task_Attack(Transform transform)
    {
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Attack");

        Transform target = (Transform)GetData("target");

        if(target != _lastTarget)
        {
            _aI_Health = target.GetComponent<AI_Health>();

            _lastTarget = target;
        }

        attackCounter += Time.deltaTime;

        if(attackCounter >= attackTime)
        {
            bool enemyIsDead = _aI_Health.TakeHit();

            if (enemyIsDead)
            {
                ClearData("target");
            }
            else
            {
                attackCounter = 0f;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
