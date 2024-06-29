using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface ITransition<TState>
    {
        TState FromState { get; }
        
        TState ToState { get; }
        
        bool IfCanConnect { get; }
        
        int Weight { get; }
        
        ITransition<TState> WithWeight(int weight);

        ITransition<TState> WithFromState(TState state);

        ITransition<TState> WithToState(TState state);
        
        ITransition<TState> AddConditions(Func<bool> condition);

        void Tick();
    }

    public class Transition<TState> : ITransition<TState>
    {
        public int Weight { get; private set; }

        private List<ICondition> _conditions;

        public bool IfCanConnect { get; private set; }
        
        public TState FromState { get; private set; }
        
        public TState ToState { get; private set; }

        public Transition()
        {
            _conditions = new List<ICondition>();
            IfCanConnect = false;
        }
        
        public ITransition<TState> WithWeight(int weight)
        {
            Weight = weight;
            return this;
        }

        public ITransition<TState> WithFromState(TState state)
        {
            FromState = state;
            return this;
        }

        public ITransition<TState> WithToState(TState state)
        {
            ToState = state;
            return this;
        }
        
        public virtual ITransition<TState> AddConditions(Func<bool> condition)
        {
            ICondition mCondition = new Condition()
                .WithCondition(condition);
            
            _conditions.Add(mCondition);
            return this;
        }

        public void Tick()
        {
            if (_conditions.Count > 0)
            {
                foreach (var condition in _conditions)
                {
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
}