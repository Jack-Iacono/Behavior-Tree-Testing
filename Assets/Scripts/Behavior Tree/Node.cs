using System.Collections.Generic;

namespace BehaviorTree
{

    public enum Status
    {
        RUNNING, SUCCESS, FAILURE
    }

    public class Node
    {
        protected Status status;

        public Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> sharedData = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }
        public Node(List<Node> children)
        {
            foreach(Node child in children)
            {
                AddChild(child);
            }
        }

        private void AddChild(Node child)
        {
            child.parent = this;
            children.Add(child);
        }

        public virtual Status Check() => Status.FAILURE;

        public void SetData(string key, object value)
        {
            sharedData[key] = value;
        }
        public object GetData(string key)
        {
            object value = null;

            if(sharedData.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if(value != null) return value;
                node = node.parent;
            }

            // At the root of the tree
            return null;
        }
        public bool ClearData(string key)
        {
            if (sharedData.ContainsKey(key))
            {
                sharedData.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared) return true;
                node = node.parent;
            }

            // At the root of the tree
            return false;
        }
    }

}
