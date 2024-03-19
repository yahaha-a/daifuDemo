namespace daifuDemo
{
    public class Selector : Sequence
    {
        protected override BehaviorNodeState OnUpdate()
        {
            if (ChildLinkedList == null)
            {
                return BehaviorNodeState.Fail;
            }

            if (CurrentChildNode.Value.Tick() == BehaviorNodeState.Fail)
            {
                ChildLinkedList.RemoveFirst();
            }
            
            if (CurrentChildNode.Value.Tick() == BehaviorNodeState.Success)
            {
                ChildLinkedList.RemoveFirst();
                return BehaviorNodeState.Success;
            }

            if (CurrentChildNode.Value.Tick() == BehaviorNodeState.Interruption)
            {
                return BehaviorNodeState.Interruption;
            }

            return BehaviorNodeState.Running;
        }
    }
}