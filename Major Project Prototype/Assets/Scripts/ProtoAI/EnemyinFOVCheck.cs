using BehaviourTree;
using UnityEngine;

public class EnemyinFOVCheck : BT_Node
{
    private static int _enemyLayerMask = 1 << 6;

    private Transform _transform;

    public EnemyinFOVCheck(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, ProtoAI.FOV_Range, _enemyLayerMask);

            if (colliders.Length > 0)
            {
                Parent.Parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
