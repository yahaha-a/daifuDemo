using System.Collections.Generic;

namespace daifuDemo
{
    public class Sequence : Composite
    {
        protected virtual LinkedListNode<IBehavior> CurrentChildNode => ChildLinkedList.First;
        
        protected override BehaviorNodeState OnUpdate()
        {
            if (CurrentChildNode == null)
            {
                return BehaviorNodeState.Success;
            }
            
            if (CurrentChildNode.Value.Tick() == BehaviorNodeState.Fail)
            {
                return BehaviorNodeState.Fail;
            }
            
            if (CurrentChildNode.Value.Tick() == BehaviorNodeState.Interruption)
            {
                return BehaviorNodeState.Interruption;
            }
            
            if (CurrentChildNode.Value.Tick() == BehaviorNodeState.Success)
            {
                ChildLinkedList.RemoveFirst();
            }

            return BehaviorNodeState.Running;
        }
    }
}