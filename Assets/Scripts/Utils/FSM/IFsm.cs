using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class FsmTransitions<TState> 
    {
        private TState _currentState;
        
        private List<ITransition<TState>> _transitions;


        public FsmTransitions()
        {
            _transitions = new List<ITransition<TState>>();
        }

        public void WithCurrentState(TState state)
        {
            _currentState = state;
        }
        
        public void AddTransitions(ITransition<TState> transition)
        {
            _transitions.Add(transition);

            _transitions.Sort((x, y) => y.Weight.CompareTo(x.Weight));
        }

        public void Tick()
        {
            if (_transitions.Count > 0)
            {
                foreach (var transition in _transitions)
                {
                    transition.Tick();
                }
            }
        }
        
        public bool IfCanChangeState()
        {
            if (_transitions.IsNotNull())
            {
                foreach (var transition in _transitions)
                {
                    if (transition.IfCanConnect)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public TState ChangeState()
        {
            if (_transitions.Count > 0)
            {
                foreach (var transition in _transitions)
                {
                    if (transition.IfCanConnect)
                    {
                        return transition.ToState;
                    }
                }
            }

            return _currentState;
        }
    }
    
    public interface IFsm
    {
        void Init();

        void Tick();
    }

    public abstract class AbstractFsm<TState> : IFsm
    {
        private TState _idleState;
        
        private TState _previousState;
        
        private TState _currentState;

        private Dictionary<TState, IState<TState>> _stateDic;

        private Dictionary<TState, FsmTransitions<TState>> _fsmTransitionDic;
        
        protected AbstractFsm()
        {
            _stateDic = new Dictionary<TState, IState<TState>>();
            _fsmTransitionDic = new Dictionary<TState, FsmTransitions<TState>>();
            Init();
        }

        public abstract void Init();
        
        protected AbstractFsm<TState> WithIdleState(TState idleState)
        {
            _idleState = idleState;
            _currentState = idleState;
            
            return this;
        }

        protected AbstractFsm<TState> AddStates(IState<TState> state)
        {
            _stateDic.Add(state.Key, state);

            return this;
        }

        protected AbstractFsm<TState> AddTransitions(ITransition<TState> intTransition)
        {
            if (!_fsmTransitionDic.ContainsKey(intTransition.FromState))
            {
                _fsmTransitionDic.Add(intTransition.FromState, new FsmTransitions<TState>());
            }

            _fsmTransitionDic[intTransition.FromState]?.AddTransitions(intTransition);

            return this;
        }

        public void Tick()
        {
            _fsmTransitionDic[_currentState].Tick();
                
            if (_fsmTransitionDic[_currentState].IfCanChangeState())
            {
                _stateDic[_currentState].Exit();
                _previousState = _currentState;
                _currentState = _fsmTransitionDic[_currentState].ChangeState();
                _stateDic[_currentState].Enter();
            }
            
            _stateDic[_currentState].Tick();
        }
    }
}
