using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace daifuDemo
{
    public class FsmTransitions<TState>
    {
        private IState<TState> _currentState;

        private IState<TState> _targetState;
        
        private List<ITransition<TState, IntConditionEnum, int>> _intTransitions;
        
        private List<ITransition<TState, FloatConditionEnum, float>> _floatTransitions;

        private List<ITransition<TState, BoolConditionEnum, bool>> _boolTransitions;

        private List<ITransition<TState, TriggerConditionEnum, Func<bool>>> _triggerTransitions;

        public FsmTransitions()
        {
            _intTransitions = new List<ITransition<TState, IntConditionEnum, int>>();
            _floatTransitions = new List<ITransition<TState, FloatConditionEnum, float>>();
            _boolTransitions = new List<ITransition<TState, BoolConditionEnum, bool>>();
            _triggerTransitions = new List<ITransition<TState, TriggerConditionEnum, Func<bool>>>();
        }

        public void WithCurrentState(IState<TState> state)
        {
            _currentState = state;
        }

        public void WithTargetState(IState<TState> state)
        {
            _targetState = state;
        }
        
        public void AddTransitions(ITransition<TState, IntConditionEnum, int> intTransition)
        {
            _intTransitions.Add(intTransition);
        }
        
        public void AddTransitions(ITransition<TState, FloatConditionEnum, float> floatTransition)
        {
            _floatTransitions.Add(floatTransition);
        }
        
        public void AddTransitions(ITransition<TState, BoolConditionEnum, bool> boolTransition)
        {
            _boolTransitions.Add(boolTransition);
        }
        
        public void AddTransitions(ITransition<TState, TriggerConditionEnum, Func<bool>> triggerTransition)
        {
            _triggerTransitions.Add(triggerTransition);
        }

        public void Tick()
        {
            foreach (var intTransition in _intTransitions)
            {
                intTransition.Tick();
            }

            foreach (var floatTransition in _floatTransitions)
            {
                floatTransition.Tick();
            }
            
            foreach (var boolTransition in _boolTransitions)
            {
                boolTransition.Tick();
            }
            
            foreach (var triggerTransition in _triggerTransitions)
            {
                triggerTransition.Tick();
            }
        }
        
        public bool IfCanChangeState()
        {
            if (_intTransitions.Count > 0)
            {
                foreach (var transition in _intTransitions)
                {
                    if (!transition.IfCanConnect)
                    {
                        return false;
                    }
                }
            }
            
            if (_floatTransitions.Count > 0)
            {
                foreach (var transition in _floatTransitions)
                {
                    if (!transition.IfCanConnect)
                    {
                        return false;
                    }
                }
            }
            
            if (_boolTransitions.Count > 0)
            {
                foreach (var transition in _boolTransitions)
                {
                    if (!transition.IfCanConnect)
                    {
                        return false;
                    }
                }
            }
            
            if (_triggerTransitions.Count > 0)
            {
                foreach (var transition in _triggerTransitions)
                {
                    if (!transition.IfCanConnect)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public IState<TState> ChangeState()
        {
            return _targetState;
        }
    }
    
    public interface IFsm
    {
        void Init();

        void Tick();
    }

    public abstract class AbstractFsm<TState> : IFsm
    {
        private IState<TState> _idleState;
        
        private IState<TState> _previousState;
        
        private IState<TState> _currentState;

        protected Dictionary<TState, IState<TState>> StateDic;

        private Dictionary<TState, FsmTransitions<TState>> _fsmTransitionDic;
        
        protected AbstractFsm()
        {
            StateDic = new Dictionary<TState, IState<TState>>();
            _fsmTransitionDic = new Dictionary<TState, FsmTransitions<TState>>();
            Init();
        }

        public abstract void Init();
        
        protected AbstractFsm<TState> WithIdleState(IState<TState> idleState)
        {
            _idleState = idleState;
            _currentState = idleState;
            
            return this;
        }

        protected AbstractFsm<TState> AddStates(IState<TState> state)
        {
            StateDic.Add(state.Key, state);

            return this;
        }

        protected AbstractFsm<TState> AddTransitions(ITransition<TState, IntConditionEnum, int> intTransition)
        {
            if (!_fsmTransitionDic.ContainsKey(intTransition.FromState.Key))
            {
                _fsmTransitionDic.Add(intTransition.FromState.Key, new FsmTransitions<TState>());
            }
            _fsmTransitionDic[intTransition.FromState.Key]?.WithCurrentState(intTransition.FromState);
            _fsmTransitionDic[intTransition.FromState.Key]?.WithTargetState(intTransition.ToState);
            _fsmTransitionDic[intTransition.FromState.Key]?.AddTransitions(intTransition);

            return this;
        }
        
        protected AbstractFsm<TState> AddTransitions(ITransition<TState, FloatConditionEnum, float> floatTransition)
        {
            if (!_fsmTransitionDic.ContainsKey(floatTransition.FromState.Key))
            {
                _fsmTransitionDic.Add(floatTransition.FromState.Key, new FsmTransitions<TState>());
            }
            _fsmTransitionDic[floatTransition.FromState.Key]?.WithCurrentState(floatTransition.FromState);
            _fsmTransitionDic[floatTransition.FromState.Key]?.WithTargetState(floatTransition.ToState);
            _fsmTransitionDic[floatTransition.FromState.Key]?.AddTransitions(floatTransition);

            return this;
        }
        
        protected AbstractFsm<TState> AddTransitions(ITransition<TState, BoolConditionEnum, bool> boolTransition)
        {
            if (!_fsmTransitionDic.ContainsKey(boolTransition.FromState.Key))
            {
                _fsmTransitionDic.Add(boolTransition.FromState.Key, new FsmTransitions<TState>());
            }
            _fsmTransitionDic[boolTransition.FromState.Key]?.WithCurrentState(boolTransition.FromState);
            _fsmTransitionDic[boolTransition.FromState.Key]?.WithTargetState(boolTransition.ToState);
            _fsmTransitionDic[boolTransition.FromState.Key]?.AddTransitions(boolTransition);

            return this;
        }
        
        protected AbstractFsm<TState> AddTransitions(ITransition<TState, TriggerConditionEnum, Func<bool>> triggerTransition)
        {
            if (!_fsmTransitionDic.ContainsKey(triggerTransition.FromState.Key))
            {
                _fsmTransitionDic.Add(triggerTransition.FromState.Key, new FsmTransitions<TState>());
            }
            _fsmTransitionDic[triggerTransition.FromState.Key]?.WithCurrentState(triggerTransition.FromState);
            _fsmTransitionDic[triggerTransition.FromState.Key]?.WithTargetState(triggerTransition.ToState);
            _fsmTransitionDic[triggerTransition.FromState.Key]?.AddTransitions(triggerTransition);

            return this;
        }

        public void Tick()
        {
            _fsmTransitionDic[_currentState.Key].Tick();

            if (_fsmTransitionDic[_currentState.Key].IfCanChangeState())
            {
                _currentState.Exit();
                _previousState = _currentState;
                _currentState = _fsmTransitionDic[_currentState.Key].ChangeState();
                _currentState.Enter();
            }
            
            _currentState.Tick();
        }
    }
}
