using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class BT_Node
    {
        protected NodeState state;

        public BT_Node Parent;

        protected List<BT_Node> children = new List<BT_Node>();

        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        public BT_Node()
        {
            Parent = null;
        }

        public BT_Node(List<BT_Node> children)
        {
            foreach (BT_Node child in children)
                attach(child);
        }

        private void attach(BT_Node node)
        {
            node.Parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;

            if (dataContext.TryGetValue(key, out value))
                return value;

            BT_Node node = Parent;

            while (node != null)
            {
                value = node.GetData(key);

                if (value != null)
                    return value;

                node = node.Parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            object value = null;

            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);

                return true;
            }

            BT_Node node = Parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);

                if (cleared)
                    return true;

                node = node.Parent;
            }
            return false;
        }
    }
}