namespace daifuDemo
{
    public class Parallel : Composite
    {
        private int _successNeedTime;

        private int _failNeedTime;

        private int _successTime;

        private int _failTime;

        public Parallel WithSuccessNeedTime(int time)
        {
            if (ChildLinkedList.Count < time)
            {
                _successNeedTime = ChildLinkedList.Count;
            }
            else
            {
                _successNeedTime = time;
            }
            
            return this;
        }

        public Parallel WithFailNeedTime(int time)
        {
            if (ChildLinkedList.Count < time)
            {
                _failNeedTime = ChildLinkedList.Count;
            }
            else
            {
                _failNeedTime = time;
            }
            
            return this;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _successTime = 0;
            _failTime = 0;
        }

        protected override BehaviorNodeState OnUpdate()
        {
            if (ChildLinkedList.Count == 0)
            {
                return BehaviorNodeState.Fail;
            }

            foreach (var behavior in ChildLinkedList)
            {
                ChildState = behavior.Tick();
                
                if (ChildState == BehaviorNodeState.Success)
                {
                    ChildLinkedList.RemoveFirst();
                    _successTime++;
                    if (_successTime == _successNeedTime)
                    {
                        return BehaviorNodeState.Success;
                    }
                }

                if (ChildState == BehaviorNodeState.Fail)
                {
                    ChildLinkedList.RemoveFirst();
                    _failTime++;
                    if (_failTime == _failNeedTime)
                    {
                        return BehaviorNodeState.Fail;
                    }
                }

                if (ChildState == BehaviorNodeState.Interruption)
                {
                    return BehaviorNodeState.Interruption;
                }
            }

            return BehaviorNodeState.Running;
        }
    }
}