using System;

namespace daifuDemo
{
    public interface IActionNode : IBehavior
    {
        
    }
    
    public class ActionNode : Behavior, IActionNode
    {
        private Func<BehaviorNodeState> _onUpdate;

        public ActionNode WithOnUpdate(Func<BehaviorNodeState> onUpdate)
        {
            _onUpdate = onUpdate;
            return this;
        }
        
        protected override BehaviorNodeState OnUpdate()
        {
            if (_onUpdate != null)
            {
                return _onUpdate();
            }
            else
            {
                return BehaviorNodeState.Success;
            }
        }
    }
}