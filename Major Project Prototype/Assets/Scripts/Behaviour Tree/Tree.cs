using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private BT_Node root = null;

        protected void Start()
        {
            root = SetUpTree();
        }

        private void Update()
        {
            if (root != null)
                root.Evaluate();
        }

        protected abstract BT_Node SetUpTree();
    }
}
