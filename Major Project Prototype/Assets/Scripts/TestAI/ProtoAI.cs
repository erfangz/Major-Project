using BehaviourTree;

public class ProtoAI : Tree
{
    public UnityEngine.Transform[] Waypoints;

    public static float Speed = 2f;

    protected override BT_Node SetUpTree()
    {
        BT_Node root = new Task_Patrol(transform, Waypoints);

        return root;
    }
}
