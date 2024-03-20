namespace daifuDemo
{
    public class Selector : Sequence
    {
        protected override BehaviorNodeState OnUpdate()
        {
            if (CurrentChildNode == null)
            {
                return BehaviorNodeState.Fail;
            }
            
            ChildState = CurrentChildNode.Value.Tick();

            if (ChildState == BehaviorNodeState.Fail)
            {
                ChildLinkedList.RemoveFirst();
            }
            
            if (ChildState == BehaviorNodeState.Success)
            {
                return BehaviorNodeState.Success;
            }

            if (ChildState == BehaviorNodeState.Interruption)
            {
                return BehaviorNodeState.Interruption;
            }

            return BehaviorNodeState.Running;
        }
    }
}