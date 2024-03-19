using System;

namespace daifuDemo
{
    public interface ICondition<TConditionType, TValueType>
    {
        bool IfSatisfyCondition { get; }
        
        ICondition<TConditionType, TValueType> WithCondition(TConditionType condition);

        ICondition<TConditionType, TValueType> WithTargetValue(TValueType targetValue);
        
        ICondition<TConditionType, TValueType> WithCurrentValue(Func<TValueType> getCurrentValue);

        void Tick();
    }

    public abstract class AbstractCondition<TConditionType, TValueType> : ICondition<TConditionType, TValueType>
    {
        protected TConditionType ConditionType;

        protected TValueType TargetValue;

        protected TValueType CurrentValue
        {
            get
            {
                return _getCurrentValue();
            }
        }

        private Func<TValueType> _getCurrentValue;

        public bool IfSatisfyCondition { get; protected set; } = false;

        public ICondition<TConditionType, TValueType> WithCondition(TConditionType conditionType)
        {
            ConditionType = conditionType;
            return this;
        }

        public ICondition<TConditionType, TValueType> WithTargetValue(TValueType targetValue)
        {
            TargetValue = targetValue;
            return this;
        }

        public ICondition<TConditionType, TValueType> WithCurrentValue(Func<TValueType> getCurrentValue)
        {
            _getCurrentValue = getCurrentValue;
            return this;
        }

        public abstract void Tick();
    }
}