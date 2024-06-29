using System;
using System.Collections.Generic;

namespace daifuDemo
{
    public interface IState<TState>
    {
        TState Key { get; }
        
        IState<TState> WithKey(TState key);

        IState<TState> WithOnEnter(Action onEnter);

        IState<TState> WithOnTick(Action onTick);

        IState<TState> WithOnExit(Action onExit);
        
        void Enter();

        void Tick();

        void Exit();
    }

    public class State<TState> : IState<TState>
    {
        public TState Key { get; private set; }
        
        private Action _onEnter;

        private Action _onTick;

        private Action _onExit;
        
        public IState<TState> WithKey(TState key)
        {
            Key = key;
            return this;
        }

        public IState<TState> WithOnEnter(Action onEnter)
        {
            _onEnter = onEnter;
            return this;
        }

        public IState<TState> WithOnTick(Action onTick)
        {
            _onTick = onTick;
            return this;
        }

        public IState<TState> WithOnExit(Action onExit)
        {
            _onExit = onExit;
            return this;
        }

        public void Enter()
        {
            _onEnter?.Invoke();
        }

        public void Tick()
        {
            _onTick?.Invoke();
        }

        public void Exit()
        {
            _onExit?.Invoke();
        }
    }
}