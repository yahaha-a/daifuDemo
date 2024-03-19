using Unity.VisualScripting;

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
        
        protected override BehaviorNodeState OnUpdate()
        {
            while (true)
            {
                if (_currentRepeatTime == _maxRepeatTime)
                {
                    return BehaviorNodeState.Success;
                }
                
                if (ChildNode.Tick() == BehaviorNodeState.Success)
                {
                    _currentRepeatTime++;
                }
                
                if (ChildNode.Tick() == BehaviorNodeState.Fail)
                {
                    return BehaviorNodeState.Fail;
                }

                if (ChildNode.Tick() == BehaviorNodeState.Interruption)
                {
                    return BehaviorNodeState.Interruption;
                }

                return BehaviorNodeState.Running;
            }
        }
    }
}