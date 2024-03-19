namespace daifuDemo
{
    public enum PlayerActionEnum
    {
        Idle,
        Walk,
        Run
    }
    
    public class PlayerBehaviorTree : BehaviorTree<PlayerActionEnum>
    {
        public override void Init()
        {
            CreateSequence().
                AddActionNode(PlayerActionEnum.Idle)
                .CreateSequence()
                    .AddActionNode(PlayerActionEnum.Idle)
                    .AddActionNode(PlayerActionEnum.Walk)
                .EndComposite()
                .CreateRepeat()
                    .CreateSequence()
                        .AddActionNode(PlayerActionEnum.Idle)
                        .AddActionNode(PlayerActionEnum.Run)
                    .EndComposite()
                .EndDecorator();
        }
    }
}