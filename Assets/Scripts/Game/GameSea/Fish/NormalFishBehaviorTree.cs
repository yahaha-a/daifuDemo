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
        HpLack,
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
        public TFish Fish { get; set; }
        
        private IPlayerModel _playerModel;
        
        public NormalFishBehaviorTree(TFish fish)
        {
            Fish = fish;
        }
        
        public override void Init()
        {
            _playerModel = this.GetModel<IPlayerModel>();
            
            this
                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.HpZero)
                    .WithOnUpdate(() =>
                    {
                        if (Fish.Hp <= 0)
                        {
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        Fish.CanSwim = false;
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.Die)
                    .WithOnUpdate(() =>
                    {
                        return BehaviorNodeState.Success;
                    })
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
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.Log("被鱼叉击中");
                        Fish.CanSwim = false;
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.Struggle)
                    .WithOnUpdate(() =>
                    {
                        Fish.IfStruggle = true;
                        
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
                        // Debug.Log("挣脱");
                        Events.FishEscape?.Trigger(Fish);
                        
                        Fish.IfStruggle = false;
                        Fish.HitByFork = false;
                        Fish.CanSwim = true;
                        Fish.CurrentStruggleTime = Fish.StruggleTime;
                        
                        _playerModel.FishingChallengeClicks.Value = 0;
                    })
                    .WithOnFailExit(() =>
                    {
                        
                    }))
                
                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.BeCaught)
                    .WithOnUpdate(() =>
                    {
                        if (_playerModel.FishingChallengeClicks.Value >= Fish.Clicks)
                        {
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.Log("被抓");
                        _playerModel.FishingChallengeClicks.Value = 0;

                        ActionKit.OnUpdate.Register(() =>
                        {
                            if (Vector3.Distance(_playerModel.CurrentPosition.Value, Fish.transform.position) <= 1f)
                            {
                                Events.CatchFish?.Trigger(Fish);
                                Fish.SendCommand<FishCountAddOneCommand>();
                                Fish.gameObject.DestroySelf();
                            }
                            else
                            {
                                var position = Fish.transform.position;
                                Fish.transform.position = Vector3.Lerp(position, _playerModel.CurrentPosition.Value,
                                    1 - Mathf.Exp(-Time.deltaTime * 30));
                            }
                        }).UnRegisterWhenGameObjectDestroyed(Fish.gameObject);
                    })
                    .WithOnFailExit(() => {}))
                
                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.HpLack)
                    .WithOnUpdate(() =>
                    {
                        if (Fish.Hp <= Fish.FleeHp)
                        {
                            return BehaviorNodeState.Success;
                        }
                        
                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.Log("血量低");
                        Fish.CanSwim = true;
                        Fish.CurrentSwimRate = Fish.FrightenedSwimRate;
                    })
                    .WithOnFailExit(() => {}))

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
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.Log("被子弹击中");
                        Fish.CanSwim = true;
                        Fish.CurrentSwimRate = Fish.FrightenedSwimRate;
                    })
                    .WithOnFailExit(() => {}))
                
                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.FrightenedSwim)
                    .WithOnUpdate(() =>
                    {
                        Fish.TargetDirection = ((Vector2)Fish.transform.position - _playerModel.CurrentPosition.Value).normalized;
                        Fish.CurrentDirection = Vector2.Lerp(Fish.CurrentDirection, Fish.TargetDirection, Time.deltaTime * 0.6f);
                        
                        return BehaviorNodeState.Success;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.Log("逃跑");
                    })
                    .WithOnFailExit(() => {}))
                
                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.DiscoverPlayer)
                    .WithOnUpdate(() =>
                    {
                        if (Fish.DiscoverPlayer)
                        {
                            return BehaviorNodeState.Success;
                        }
                        
                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.Log("发现玩家");
                        Fish.CurrentSwimRate = Fish.FrightenedSwimRate;
                        
                    })
                    .WithOnFailExit(() =>
                    {
                        // Debug.Log("没发现玩家");
                    }))
                
                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.Swim)
                    .WithOnUpdate(() =>
                    {
                        // Debug.Log("游动");
                        Fish.CanSwim = true;
                        Fish.CurrentSwimRate = Fish.SwimRate;
                        
                        if (Vector3.Distance(Fish.transform.position, Fish.StartPosition) >= Fish.RangeOfMovement)
                        {
                            Vector3 baseEscapeDirection = -Fish.CurrentDirection;
                            float randomOffsetX = Random.Range(-0.3f, 0.3f);
                            float randomOffsetY = Random.Range(-0.3f, 0.3f);
                            Vector3 randomOffset = new Vector3(randomOffsetX, randomOffsetY, 0);
                            Fish.CurrentDirection = (baseEscapeDirection + randomOffset).normalized;
                        }
                        else if (Fish.CurrentToggleDirectionTime <= 0)
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
                    .WithOnFailExit(() => {}));
            
            CreateSequence()
                .CreateInverter()
                    .CreateSequence()
                        .AddAction(NormalFishBehaviorTreeEnum.HpZero)
                        .AddAction(NormalFishBehaviorTreeEnum.Die)
                    .EndComposite()
                .EndDecorator()
                .CreateInverter()
                    .CreateSequence()
                        .AddAction(NormalFishBehaviorTreeEnum.HitByFork)
                        .CreateInverter()
                            .CreateSelector()
                                .AddAction(NormalFishBehaviorTreeEnum.Struggle)
                                .AddAction(NormalFishBehaviorTreeEnum.BeCaught)
                            .EndComposite()
                        .EndDecorator()
                    .EndComposite()
                .EndDecorator()
                .CreateInverter()
                    .CreateSequence()
                        .CreateSelector()
                            .AddAction(NormalFishBehaviorTreeEnum.DiscoverPlayer)
                            .AddAction(NormalFishBehaviorTreeEnum.HpLack)
                            .AddAction(NormalFishBehaviorTreeEnum.HitByBullet)
                        .EndComposite()
                        .AddAction(NormalFishBehaviorTreeEnum.FrightenedSwim)
                    .EndComposite()
                .EndDecorator()
                .AddAction(NormalFishBehaviorTreeEnum.Swim)
            .EndComposite();
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}