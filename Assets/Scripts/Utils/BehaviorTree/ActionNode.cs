using System;

namespace daifuDemo
{
    public interface IActionNode<TType> : IBehavior
        where TType : Enum
    {
        TType ActionType { get; }
        
        IActionNode<TType> WithActionType(TType type);
        
        IActionNode<TType> WithOnUpdate(Func<BehaviorNodeState> onUpdate);

        new IActionNode<TType> WithOnStart(Action onStart);

        new IActionNode<TType> WithOnSuccessExit(Action action);

        new IActionNode<TType> WithOnFailExit(Action action);

        new IActionNode<TType> WithOnInterruption(Action action);
    }
    
    public class ActionNode<TType> : Behavior, IActionNode<TType>
        where TType : Enum
    {
        private Func<BehaviorNodeState> _onUpdate;

        public TType ActionType { get; private set; }

        public IActionNode<TType> WithActionType(TType type)
        {
            ActionType = type;
            return this;
        }

        public IActionNode<TType> WithOnUpdate(Func<BehaviorNodeState> onUpdate)
        {
            _onUpdate = onUpdate;
            return this;
        }

        public new IActionNode<TType> WithOnStart(Action onStart)
        {
            _onStart = onStart;
            return this;
        }

        public new IActionNode<TType> WithOnSuccessExit(Action action)
        {
            _onSuccessExit = action;
            return this;
        }

        public new IActionNode<TType> WithOnFailExit(Action action)
        {
            _onFailExit = action;
            return this;
        }

        public new IActionNode<TType> WithOnInterruption(Action action)
        {
            _onInterruption = action;
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