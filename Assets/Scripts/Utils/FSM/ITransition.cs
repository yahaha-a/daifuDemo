using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface ITransition<TState, TConditionType, TValueType>
    {
        IState<TState> FromState { get; }
        
        IState<TState> ToState { get; }
        
        bool IfCanConnect { get; }
        
        int Weight { get; }
        
        ITransition<TState, TConditionType, TValueType> WithWeight(int weight);

        ITransition<TState, TConditionType, TValueType> WithFromState(IState<TState> state);

        ITransition<TState, TConditionType, TValueType> WithToState(IState<TState> state);
        
        ITransition<TState, TConditionType, TValueType> AddConditions(
            ICondition<TConditionType, TValueType> condition);

        void Tick();
    }

    public abstract class Transition<TState, TConditionType, TValueType> : ITransition<TState, TConditionType, TValueType>
    {
        public int Weight { get; private set; }

        private List<ICondition<TConditionType, TValueType>> _conditions;

        public bool IfCanConnect { get; private set; }
        
        public IState<TState> FromState { get; private set; }
        
        public IState<TState> ToState { get; private set; }

        protected Transition()
        {
            _conditions = new List<ICondition<TConditionType, TValueType>>();
            IfCanConnect = false;
        }
        
        public ITransition<TState, TConditionType, TValueType> WithWeight(int weight)
        {
            Weight = weight;
            return this;
        }

        public ITransition<TState, TConditionType, TValueType> WithFromState(IState<TState> state)
        {
            FromState = state;
            return this;
        }

        public ITransition<TState, TConditionType, TValueType> WithToState(IState<TState> state)
        {
            ToState = state;
            return this;
        }

        public virtual ITransition<TState, TConditionType, TValueType> AddConditions(ICondition<TConditionType, TValueType> condition)
        {
            _conditions.Add(condition);
            return this;
        }

        public void Tick()
        {
            if (_conditions.IsNotNull())
            {
                foreach (var condition in _conditions)
                {
                    condition.Tick();
                    if (!condition.IfSatisfyCondition)
                    {
                        IfCanConnect = false;
                        return;
                    }
                }
                IfCanConnect = true;
            }
            else
            {
                IfCanConnect = true;
            }
        }
    }

    public class FloatTransition<TState> : Transition<TState, FloatConditionEnum, float>
    {
        
    }
    
    public class IntTransition<TState> : Transition<TState, IntConditionEnum, int>
    {
        
    }
    
    public class BoolTransition<TState> : Transition<TState, BoolConditionEnum, bool>
    {
        
    }
    
    public class TriggerTransition<TState> : Transition<TState, TriggerConditionEnum, Func<bool>>
    {
        
    }
}