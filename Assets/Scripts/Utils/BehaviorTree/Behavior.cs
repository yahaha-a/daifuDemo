using System;
using UnityEngine;

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

        IBehavior WithOnStart(Action onStart);

        IBehavior WithOnSuccessExit(Action action);

        IBehavior WithOnFailExit(Action action);

        IBehavior WithOnInterruption(Action action);
    }
    
    public abstract class Behavior : IBehavior
    {
        private BehaviorNodeState _currentState;
        protected BehaviorNodeState ChildState;
        private readonly BehaviorNodeState _idleState = BehaviorNodeState.NotStart;

        private bool IfSuccess => _currentState == BehaviorNodeState.Success;
        private bool IfFail => _currentState == BehaviorNodeState.Fail;
        private bool IfRunning => _currentState == BehaviorNodeState.Running;
        private bool IfNotStart => _currentState == BehaviorNodeState.NotStart;
        private bool IfInterruption => _currentState == BehaviorNodeState.Interruption;
        
        protected Action _onStart;
        protected Action _onSuccessExit;
        protected Action _onFailExit;
        protected Action _onInterruption;
        
        protected Behavior()
        {
            _currentState = _idleState;
        }

        public IBehavior WithOnStart(Action onStart)
        {
            _onStart = onStart;
            return this;
        }
        
        public IBehavior WithOnSuccessExit(Action action)
        {
            _onSuccessExit = action;
            return this;
        }
        
        public IBehavior WithOnFailExit(Action action)
        {
            _onFailExit = action;
            return this;
        }
        
        public IBehavior WithOnInterruption(Action action)
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

        protected virtual void Initialize()
        {
            _currentState = _idleState;
        }

        public BehaviorNodeState Tick()
        {
            if (IfNotStart)
            {
                OnStart();
                _currentState = BehaviorNodeState.Running;
                return BehaviorNodeState.Running;
            }
            
            if (IfRunning)
            {
                _currentState = OnUpdate();
            }

            if (IfSuccess)
            {
                OnSuccessExit();
                Initialize();
                return BehaviorNodeState.Success;
            }

            if (IfFail)
            {
                OnFailExit();
                Initialize();
                return BehaviorNodeState.Fail;
            }

            if (IfInterruption)
            {
                OnInterruption();
                Initialize();
                return BehaviorNodeState.Interruption;
            }

            return _currentState;
        }
    }
}