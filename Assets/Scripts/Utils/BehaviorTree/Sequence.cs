using System.Collections.Generic;
using UnityEngine;

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
            
            ChildState = CurrentChildNode.Value.Tick();
            
            if (ChildState == BehaviorNodeState.Fail)
            {
                ChildLinkedList.RemoveFirst();
                return BehaviorNodeState.Fail;
            }
            
            if (ChildState == BehaviorNodeState.Interruption)
            {
                return BehaviorNodeState.Interruption;
            }
            
            if (ChildState == BehaviorNodeState.Success)
            {
                ChildLinkedList.RemoveFirst();
            }

            return BehaviorNodeState.Running;
        }
    }
}