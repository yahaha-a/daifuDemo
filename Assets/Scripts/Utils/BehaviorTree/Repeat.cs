using Unity.VisualScripting;
using UnityEngine;

namespace daifuDemo
{
    public class Repeat : Decorator
    {
        private int _maxRepeatTime;

        private int _currentRepeatTime;

        public void WithMaxRepeatTime(int time)
        {
            _maxRepeatTime = time;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _currentRepeatTime = 0;
        }

        protected override BehaviorNodeState OnUpdate()
        {
            ChildState = ChildNode.Tick();
                
            if (ChildState == BehaviorNodeState.Success)
            {
                _currentRepeatTime++;
            }
            
            if (_currentRepeatTime == _maxRepeatTime)
            {
                return BehaviorNodeState.Success;
            }
                
            if (ChildState == BehaviorNodeState.Fail)
            {
                return BehaviorNodeState.Fail;
            }

            if (ChildState == BehaviorNodeState.Interruption)
            {
                return BehaviorNodeState.Interruption;
            }

            return BehaviorNodeState.Running;
        }
    }
}