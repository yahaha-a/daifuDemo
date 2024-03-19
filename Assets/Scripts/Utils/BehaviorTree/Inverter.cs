namespace daifuDemo
{
    public class Inverter : Decorator
    {
        protected override BehaviorNodeState OnUpdate()
        {
            if (ChildNode.Tick() == BehaviorNodeState.Success)
            {
                return BehaviorNodeState.Fail;
            }

            if (ChildNode.Tick() == BehaviorNodeState.Fail)
            {
                return BehaviorNodeState.Success;
            }
            
            if (ChildNode.Tick() == BehaviorNodeState.Interruption)
            {
                return BehaviorNodeState.Interruption;
            }

            return BehaviorNodeState.Running;
        }
    }
}