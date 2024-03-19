namespace daifuDemo
{
    public class Parallel : Composite
    {
        protected int SuccessNeedTime;

        protected int FailNeedTime;

        protected int SuccessTime;

        protected int FailTime;

        public Behavior WithSuccessNeedTime(int time)
        {
            if (ChildLinkedList.Count < time)
            {
                SuccessNeedTime = ChildLinkedList.Count;
            }
            else
            {
                SuccessNeedTime = time;
            }
            
            return this;
        }

        public Behavior WithFailNeedTime(int time)
        {
            if (ChildLinkedList.Count < time)
            {
                SuccessNeedTime = ChildLinkedList.Count;
            }
            else
            {
                FailNeedTime = time;
            }
            
            return this;
        }
        
        protected override BehaviorNodeState OnUpdate()
        {
            if (ChildLinkedList.Count == 0)
            {
                return BehaviorNodeState.Fail;
            }

            foreach (var behavior in ChildLinkedList)
            {
                if (behavior.Tick() == BehaviorNodeState.Success)
                {
                    ChildLinkedList.RemoveFirst();
                    SuccessTime++;
                    if (SuccessTime == SuccessNeedTime)
                    {
                        return BehaviorNodeState.Success;
                    }
                }

                if (behavior.Tick() == BehaviorNodeState.Fail)
                {
                    ChildLinkedList.RemoveFirst();
                    FailTime++;
                    if (FailTime == FailNeedTime)
                    {
                        return BehaviorNodeState.Fail;
                    }
                }
            }

            return BehaviorNodeState.Running;
        }
    }
}