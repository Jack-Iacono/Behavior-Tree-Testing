using System.Collections.Generic;

namespace BehaviorTree
{
    /// <summary>
    /// Will check nodes until one returns either Failure or Running, it will then exit
    /// </summary>
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override Status Check()
        {
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
                        status = Status.RUNNING;
                        return status;
                    default:
                        status = Status.SUCCESS;
                        return status;
                }
            }

            status = Status.SUCCESS;
            return status;
        }
    }
}
