namespace daifuDemo
{
    public class Inverter : Decorator
    {
        protected override BehaviorNodeState OnUpdate()
        {
            ChildState = ChildNode.Tick();
            
            if (ChildState == BehaviorNodeState.Success)
            {
                return BehaviorNodeState.Fail;
            }

            if (ChildState == BehaviorNodeState.Fail)
            {
                return BehaviorNodeState.Success;
            }
            
            if (ChildState == BehaviorNodeState.Interruption)
            {
                return BehaviorNodeState.Interruption;
            }

            return BehaviorNodeState.Running;
        }
    }
}