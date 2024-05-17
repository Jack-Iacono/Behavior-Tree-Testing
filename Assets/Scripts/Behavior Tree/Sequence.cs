using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override Status Check()
        {
            bool childRunning = false;

            foreach(Node node in children)
            {
                switch (node.Check())
                {
                    case Status.FAILURE:
                        status = Status.FAILURE;
                        return status;
                    case Status.SUCCESS:
                        continue;
                    case Status.RUNNING:
                        childRunning = true;
                        continue;
                    default:
                        status = Status.SUCCESS;
                        return status;
                }
            }

            status = childRunning ? Status.RUNNING : Status.SUCCESS;
            return status;
        }
    }
}
