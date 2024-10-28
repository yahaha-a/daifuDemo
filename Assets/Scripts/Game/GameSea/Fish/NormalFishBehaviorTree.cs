using System;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace daifuDemo
{
    public enum NormalFishBehaviorTreeEnum
    {
        Die,
        Struggle,
        Swim,
        FrightenedSwim,
        BeCaught,
        
        HpZero,
        HitByFork,
        DiscoverPlayer,
        HitByBullet
    }

    public interface IFishBehaviorTree<TFish> where TFish : IFish 
    {
        TFish Fish { get; set; }
    }
    
    public class NormalFishBehaviorTree<TFish> : BehaviorTree<NormalFishBehaviorTreeEnum>, IFishBehaviorTree<TFish>, IController where TFish : NormalFish
    {
        private IUtils _utils;
        
        public TFish Fish { get; set; }
        
        public Player Player { get; set; }
        
        public NormalFishBehaviorTree(TFish fish, Player player)
        {
            Fish = fish;
            Player = player;
        }
        
        public override void Init()
        {
            _utils = this.GetUtility<IUtils>();
            
            this
                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.HpZero)
                    .WithOnUpdate(() =>
                    {
                        if (Fish.Hp <= 0)
                        {
                            Fish.CanSwim = false;
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.Die)
                    .WithOnUpdate(null)
                    .WithOnSuccessExit(() =>
                    {
                        Fish.gameObject.DestroySelf();
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.HitByFork)
                    .WithOnUpdate(() =>
                    {
                        if (Fish.HitByFork)
                        {
                            Fish.CanSwim = false;
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.Struggle)
                    .WithOnUpdate(() =>
                    {
                        Fish.State = FishState.Struggle;
                        
                        Vector2 randomDirection = new Vector3(
                            Random.Range(-1f, 1f),
                            Random.Range(-1f, 1f)
                        ).normalized;
                        
                        float currentAmplitude = 0.1f;
                        Vector2 moveVector = randomDirection * currentAmplitude;
                        Fish.transform.position = Fish.HitPosition + moveVector;
                        
                        if (Fish.CurrentStruggleTime <= 0)
                        {
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        Events.FishEscape?.Trigger(Fish);
                        Fish.State = FishState.Swim;
                        Fish.HitByFork = false;
                        Fish.CurrentStruggleTime = Fish.StruggleTime;
                        Player.ResetFishChallengeClicks();
                    })
                    .WithOnFailExit(() =>
                    {
                        
                    }))
                
                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.BeCaught)
                    .WithOnUpdate(() =>
                    {
                        if (Player.GetFishChallengeClicks >= Fish.Clicks)
                        {
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        Player.ResetFishChallengeClicks();
                        
                        ActionKit.OnUpdate.Register(() =>
                        {
                            if (Vector3.Distance(Player.transform.position, Fish.transform.position) <= 1f)
                            {
                                Events.CatchFish?.Trigger(Fish);
                                Fish.SendCommand<FishCountAddOneCommand>();
                                Fish.gameObject.DestroySelf();
                            }
                            else
                            {
                                var position = Fish.transform.position;
                                Fish.transform.position = Vector3.Lerp(position, Player.transform.position, 1 - Mathf.Exp(-Time.deltaTime * 30));
                            }
                        }).UnRegisterWhenGameObjectDestroyed(Fish.gameObject);
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.Swim)
                    .WithOnUpdate(() =>
                    {
                        Fish.CanSwim = true;
                        
                        if (Fish.CurrentToggleDirectionTime <= 0)
                        {
                            Fish.CurrentToggleDirectionTime = Fish.ToggleDirectionTime;
                            Fish.TargetDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                            while (Fish.TargetDirection.magnitude == 0)
                            {
                                Fish.TargetDirection =
                                    new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                            }
                            Fish.CurrentDirection = Fish.TargetDirection;
                        }
                        
                        return BehaviorNodeState.Success;
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.DiscoverPlayer)
                    .WithOnUpdate(() =>
                    {
                        if (Fish.DiscoverPlayer)
                        {
                            return BehaviorNodeState.Success;
                        }
                        else
                        {
                            return BehaviorNodeState.Fail;
                        }
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() =>
                    {
                        if (Vector3.Distance(Fish.transform.position, Fish.StartPosition) >= Fish.RangeOfMovement)
                        {
                            Vector3 baseEscapeDirection = -Fish.CurrentDirection;
                            float randomOffsetX = Random.Range(-0.3f, 0.3f);
                            float randomOffsetY = Random.Range(-0.3f, 0.3f);
                            Vector3 randomOffset = new Vector3(randomOffsetX, randomOffsetY, 0);
                            Fish.CurrentDirection = (baseEscapeDirection + randomOffset).normalized;
                        }
                        Fish.CurrentSwimRate = Fish.SwimRate;
                    }))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.HitByBullet)
                    .WithOnUpdate(() =>
                    {
                        if (Fish.HitByBullet)
                        {
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.FrightenedSwim)
                    .WithOnUpdate(() =>
                    {
                        Vector3 baseEscapeDirection = (Fish.transform.position - Player.transform.position).normalized;
                        float randomOffsetX = Random.Range(-0.3f, 0.3f);
                        float randomOffsetY = Random.Range(-0.3f, 0.3f);
                        Vector3 randomOffset = new Vector3(randomOffsetX, randomOffsetY, 0);
                        Fish.TargetDirection = (baseEscapeDirection + randomOffset).normalized;
                        Fish.CurrentDirection = Vector3.Lerp(Fish.CurrentDirection, Fish.TargetDirection, Time.deltaTime * 0.6f);
                        
                        Fish.CurrentSwimRate = Fish.FrightenedSwimRate;

                        return BehaviorNodeState.Success;
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}));
            
            CreateSequence()
                .CreateInverter()
                    .CreateSelector()
                        .CreateSequence()
                            .AddAction(NormalFishBehaviorTreeEnum.HpZero)
                            .AddAction(NormalFishBehaviorTreeEnum.Die)
                        .EndComposite()
                        .CreateSequence()
                            .AddAction(NormalFishBehaviorTreeEnum.HitByFork)
                            .CreateInverter()
                                .CreateSelector()
                                    .AddAction(NormalFishBehaviorTreeEnum.Struggle)
                                    .AddAction(NormalFishBehaviorTreeEnum.BeCaught)
                                .EndComposite()
                            .EndDecorator()
                        .EndComposite()
                    .EndComposite()
                .EndDecorator()
                .CreateSequence()
                    .AddAction(NormalFishBehaviorTreeEnum.Swim)
                    .CreateSelector()
                        .AddAction(NormalFishBehaviorTreeEnum.DiscoverPlayer)
                        .AddAction(NormalFishBehaviorTreeEnum.HitByBullet)
                    .EndComposite()
                .EndComposite()
                .AddAction(NormalFishBehaviorTreeEnum.FrightenedSwim)
            .EndComposite();
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}