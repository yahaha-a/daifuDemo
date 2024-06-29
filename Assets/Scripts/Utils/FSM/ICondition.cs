using System;

namespace daifuDemo
{
    public interface ICondition
    {
        bool IfSatisfyCondition { get; }
        
        ICondition WithCondition(Func<bool> getCurrentValue);
    }

    public class Condition : ICondition
    {
        private Func<bool> _condition;

        public bool IfSatisfyCondition => _condition();

        public ICondition WithCondition(Func<bool> condition)
        {
            _condition = condition;
            return this;
        }
    }
}