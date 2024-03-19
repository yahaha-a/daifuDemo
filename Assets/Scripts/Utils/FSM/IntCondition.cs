namespace daifuDemo
{
    public enum IntConditionEnum
    {
        Greater,
        Less,
        Equal,
        NotEqual
    }
    
    public class IntCondition : AbstractCondition<IntConditionEnum, int>
    {
        public IntCondition() : base()
        {
            
        }
        
        public override void Tick()
        {
            switch (ConditionType)
            {
                case IntConditionEnum.Greater:
                    if (CurrentValue > TargetValue)
                    {
                        IfSatisfyCondition = true;
                    }
                    else
                    {
                        IfSatisfyCondition = false;
                    }
                    break;
                case IntConditionEnum.Less:
                    if (CurrentValue < TargetValue)
                    {
                        IfSatisfyCondition = true;
                    }
                    else
                    {
                        IfSatisfyCondition = false;
                    }
                    break;
                case IntConditionEnum.Equal:
                    if (CurrentValue == TargetValue)
                    {
                        IfSatisfyCondition = true;
                    }
                    else
                    {
                        IfSatisfyCondition = false;
                    }
                    break;
                case IntConditionEnum.NotEqual:
                    if (CurrentValue != TargetValue)
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