using System.Collections.Generic;

namespace BehaviorTree
{
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
