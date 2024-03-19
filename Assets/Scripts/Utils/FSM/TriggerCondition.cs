using System;
using UnityEngine;

namespace daifuDemo
{
    public enum TriggerConditionEnum
    {
        Once,
        Repeatedly
    }
    
    public class TriggerCondition : AbstractCondition<TriggerConditionEnum, Func<bool>>
    {
        private bool _ifCanExecute = true;

        public TriggerCondition() : base()
        {
            
        }

        public override void Tick()
        {
            if (_ifCanExecute)
            {
                switch (ConditionType)
                {
                    case TriggerConditionEnum.Once:
                        if (CurrentValue())
                        {
                            IfSatisfyCondition = true;
                            _ifCanExecute = false;
                        }
                        else
                        {
                            IfSatisfyCondition = false;
                        }
                        break;
                    case TriggerConditionEnum.Repeatedly:
                        if (CurrentValue())
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
}