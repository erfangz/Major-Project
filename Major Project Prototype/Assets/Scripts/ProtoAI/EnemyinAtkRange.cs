using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class EnemyinAtkRange : BT_Node
{
    private static int _enemyLayerMask = 1 << 6;

    private Transform _transform;

    public EnemyinAtkRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {

        object t = GetData("target");

        if (t == null)
        {
            state = NodeState.FAILURE;
            Debug.Log("In Attack range" + state.ToString());
            return state;
        }

        Transform target = (Transform)t;

        if (Vector3.Distance(_transform.position, target.position) <= ProtoAI.Atk_Range)
        {
            state = NodeState.SUCCESS;
            Debug.Log("In Attack range" + state.ToString());
            return state;
        }

        state = NodeState.FAILURE;
        Debug.Log("In Attack range" + state.ToString());
        return state;
    }
}
