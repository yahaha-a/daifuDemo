using System;

namespace daifuDemo
{
    public enum FloatConditionEnum
    {
        Greater,
        Less,
        Equal,
        NotEqual
    }
    
    public class FloatCondition : AbstractCondition<FloatConditionEnum, float>
    {
        public FloatCondition() : base()
        {
            
        }
        
        public override void Tick()
        {
            switch (ConditionType)
            {
                case FloatConditionEnum.Greater:
                    if (CurrentValue > TargetValue)
                    {
                        IfSatisfyCondition = true;
                    }
                    else
                    {
                        IfSatisfyCondition = false;
                    }
                    break;
                case FloatConditionEnum.Less:
                    if (CurrentValue < TargetValue)
                    {
                        IfSatisfyCondition = true;
                    }
                    else
                    {
                        IfSatisfyCondition = false;
                    }
                    break;
                case FloatConditionEnum.Equal:
                    if (Math.Abs(CurrentValue - TargetValue) < 0.01f)
                    {
                        IfSatisfyCondition = true;
                    }
                    else
                    {
                        IfSatisfyCondition = false;
                    }
                    break;
                case FloatConditionEnum.NotEqual:
                    if (Math.Abs(CurrentValue - TargetValue) >= 0.01f)
                    {
                        IfSatisfyCondition = true;
                    }
                    else
                    {
                        IfSatisfyCondition = false;
                    }
                    break;
            }
        }
    }
}