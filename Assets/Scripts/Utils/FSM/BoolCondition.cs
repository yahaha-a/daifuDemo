using System;

namespace daifuDemo
{
    public enum BoolConditionEnum
    {
        True,
        False
    }
    
    public class BoolCondition : AbstractCondition<BoolConditionEnum, bool>
    {
        public BoolCondition() : base()
        {
            
        }
        
        public override void Tick()
        {
            switch (ConditionType)
            {
                case BoolConditionEnum.True:
                    if (CurrentValue == true)
                    {
                        IfSatisfyCondition = true;
                    }
                    else
                    {
                        IfSatisfyCondition = false;
                    }
                    break;
                case BoolConditionEnum.False:
                    if (CurrentValue == false)
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