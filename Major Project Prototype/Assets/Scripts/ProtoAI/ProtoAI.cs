using BehaviourTree;
using System.Collections.Generic;

public class ProtoAI : Tree
{
    public UnityEngine.Transform[] Waypoints;

    public static float Speed = 2f;
    public static float FOV_Range = 6f;

    protected override BT_Node SetUpTree()
    {
        BT_Node root = new Selector(new List<BT_Node>
        {
            new Sequence(new List<BT_Node>
            {
                new EnemyinFOVCheck(transform),
                new Task_GoToTarget(transform),
            }),

            new Task_Patrol(transform, Waypoints),
        });

        return root;
    }
}
