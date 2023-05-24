using System.Collections.Generic;

namespace BehaviourTree
{
    public class Selector : BT_Node
    {
        public Selector() : base() { }
        public Selector(List<BT_Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (BT_Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        continue;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        continue;

                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }

}
