using System;

namespace daifuDemo
{
    public enum BehaviorNodeState
    {
        Success,
        Fail,
        Running,
        NotStart,
        Interruption
    }
    
    public interface IBehavior
    {
        BehaviorNodeState Tick();
    }
    
    public abstract class Behavior : IBehavior
    {
        public int Weight;
        
        private BehaviorNodeState _currentState;
        private readonly BehaviorNodeState _idleState = BehaviorNodeState.NotStart;

        private bool IfSuccess => _currentState == BehaviorNodeState.Success;
        private bool IfFail => _currentState == BehaviorNodeState.Fail;
        private bool IfRunning => _currentState == BehaviorNodeState.Running;
        private bool IfNotStart => _currentState == BehaviorNodeState.NotStart;

        private bool IfInterruption => _currentState == BehaviorNodeState.Interruption;
        
        private Action _onStart;
        private Action _onSuccessExit;
        private Action _onFailExit;
        private Action _onInterruption;
        
        protected Behavior()
        {
            _currentState = _idleState;
        }

        protected Behavior WithOnStart(Action onStart)
        {
            _onStart = onStart;
            return this;
        }
        
        protected Behavior WithOnSuccessExit(Action action)
        {
            _onSuccessExit = action;
            return this;
        }
        
        protected Behavior WithOnFailExit(Action action)
        {
            _onFailExit = action;
            return this;
        }
        
        protected Behavior WithOnInterruption(Action action)
        {
            _onInterruption = action;
            return this;
        }

        protected virtual void OnStart()
        {
            _onStart?.Invoke();
        }

        protected abstract BehaviorNodeState OnUpdate();

        protected virtual void OnSuccessExit()
        {
            _onSuccessExit?.Invoke();
        }

        protected virtual void OnFailExit()
        {
            _onFailExit?.Invoke();
        }

        protected virtual void OnInterruption()
        {
            _onInterruption?.Invoke();
        }

        public BehaviorNodeState Tick()
        {
            if (IfNotStart)
            {
                OnStart();
            }

            if (IfRunning)
            {
                _currentState = OnUpdate();
            }

            if (IfSuccess)
            {
                OnSuccessExit();
            }

            if (IfFail)
            {
                OnFailExit();
            }

            if (IfInterruption)
            {
                OnInterruption();
            }

            return _currentState;
        }
    }
}