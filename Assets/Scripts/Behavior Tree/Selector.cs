using System.Collections.Generic;

namespace BehaviorTree
{
    /// <summary>
    /// Will check nodes until one returns either SUCCESS or RUNNING, it will then exit
    /// Will not evaluate the nodes if the selector node returns FAILURE
    /// </summary>
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override Status Check()
        {
            foreach (Node node in children)
            {
                switch (node.Check())
                {
                    case Status.FAILURE:
                        continue;
                    case Status.SUCCESS:
                        status = Status.SUCCESS;
                        return status;
                    case Status.RUNNING:
                        status = Status.RUNNING;
                        return status;
                    default:
                        continue;
                }
            }

            status = Status.FAILURE;
            return status;
        }
    }
}
