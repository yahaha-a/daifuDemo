using UnityEngine;

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
            this.AddActionNodeDic(
                    new ActionNode<PlayerActionEnum>()
                        .WithActionType(PlayerActionEnum.Idle)
                        .WithOnStart(() => Debug.Log("进入站立"))
                        .WithOnSuccessExit(() => Debug.Log("离开站立成功"))
                        .WithOnFailExit(() => Debug.Log("离开站立失败"))
                        .WithOnInterruption(null)
                        .WithOnUpdate(() => { return BehaviorNodeState.Fail;}))
                .AddActionNodeDic(
                    new ActionNode<PlayerActionEnum>()
                        .WithActionType(PlayerActionEnum.Walk)
                        .WithOnStart(() => Debug.Log("进入行走"))
                        .WithOnSuccessExit(() => Debug.Log("离开行走成功"))
                        .WithOnFailExit(() => Debug.Log("离开行走失败"))
                        .WithOnInterruption(null)
                        .WithOnUpdate(null))
                .AddActionNodeDic(
                    new ActionNode<PlayerActionEnum>()
                        .WithActionType(PlayerActionEnum.Run)
                        .WithOnStart(() => Debug.Log("进入跑步"))
                        .WithOnSuccessExit(() => Debug.Log("离开跑步成功"))
                        .WithOnFailExit(() => Debug.Log("离开跑步失败"))
                        .WithOnInterruption(null)
                        .WithOnUpdate(null));

            // CreateSequence()
            //     .CreateSequence()
            //         .AddAction(PlayerActionEnum.Idle)
            //     .EndComposite()
            //     .CreateRepeat(5)
            //         .CreateSequence()
            //             .AddAction(PlayerActionEnum.Run)
            //             .CreateSelector()
            //                 .AddAction(PlayerActionEnum.Walk)
            //             .EndComposite()
            //         .EndComposite()
            //     .EndDecorator()
            // .EndComposite();

            // CreateSelector()
            //     .AddAction(PlayerActionEnum.Idle)
            //     .AddAction(PlayerActionEnum.Run)
            //     .AddAction(PlayerActionEnum.Walk);
            
            CreateSequence()
                .CreateSelector()
                    .AddAction(PlayerActionEnum.Idle)
                    .AddAction(PlayerActionEnum.Run)
                .EndComposite()
                .CreateSequence()
                    .AddAction(PlayerActionEnum.Walk)
                .EndComposite();
        }
    }
}