using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum PteroisBehaviorTreeEnum
    {
        Die,
        Struggle,
        Swim,
        FrightenedSwim,
        Chase,
        Attack,
        BeCaught,
        
        HpZero,
        HpLack,
        HitByFork,
        DiscoverPlayer,
        HitByBullet
    }
    
    public class PteroisFishBehaviorTree<TFish> : BehaviorTree<PteroisBehaviorTreeEnum>, IFishBehaviorTree<TFish>, IController where TFish : Pterois
    {
        public TFish Fish { get; set; }

        private IPlayerModel _playerModel;
        
        public PteroisFishBehaviorTree(TFish fish)
        {
            Fish = fish;
        }
        
        public override void Init()
        {
            _playerModel = this.GetModel<IPlayerModel>();
            
            this
                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.HpZero)
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

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Die)
                    .WithOnUpdate(() =>
                    {
                        return BehaviorNodeState.Success;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        Fish.gameObject.DestroySelf();
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.HitByFork)
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

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Struggle)
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

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.BeCaught)
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
                
                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.HpLack)
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
                
                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.HitByBullet)
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
                
                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.FrightenedSwim)
                    .WithOnUpdate(() =>
                    {
                        Vector2 baseEscapeDirection = ((Vector2)Fish.transform.position - _playerModel.CurrentPosition.Value).normalized;
                        float randomOffsetX = Random.Range(-0.3f, 0.3f);
                        float randomOffsetY = Random.Range(-0.3f, 0.3f);
                        Vector2 randomOffset = new Vector2(randomOffsetX, randomOffsetY);
                        Fish.TargetDirection = (baseEscapeDirection + randomOffset).normalized;
                        Fish.CurrentDirection = Vector2.Lerp(Fish.CurrentDirection, Fish.TargetDirection, Time.deltaTime * 0.6f);
                        
                        return BehaviorNodeState.Success;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.Log("逃跑");
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.DiscoverPlayer)
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
                        Fish.CurrentDirection = (_playerModel.CurrentPosition.Value - (Vector2)Fish.transform.position).normalized;
                        Fish.CurrentSwimRate = Fish.PursuitSwimRate;
                        
                    })
                    .WithOnFailExit(() =>
                    {
                        // Debug.Log("没发现玩家");
                    }))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Chase)
                    .WithOnUpdate(() =>
                    {
                        if (Vector3.Distance(_playerModel.CurrentPosition.Value, Fish.transform.position) <
                            Fish.AttackRange && Fish.CurrentAttackInterval <= 0f)
                        {
                            return BehaviorNodeState.Success;
                        }
                        
                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.Log("准备攻击");
                        Fish.CanSwim = false;
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Attack)
                    .WithOnUpdate(() =>
                    {
                        Fish.IfCharge = true;
                        if (Fish.CurrentChargeTime <= 0)
                        {
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        // Debug.LogWarning("攻击");
                        Fish.IfAttack = true;
                        Fish.CurrentAttackInterval = Fish.AttackInterval;
                        Fish.CurrentChargeTime = Fish.ChargeTime;
                        Fish.CurrentAttackTargetPosition = _playerModel.CurrentPosition.Value;
                    })
                    .WithOnFailExit(() => {}))
                
                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Swim)
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
                        .AddAction(PteroisBehaviorTreeEnum.HpZero)
                        .AddAction(PteroisBehaviorTreeEnum.Die)
                    .EndComposite()
                .EndDecorator()
                .CreateInverter()
                    .CreateSequence()
                        .AddAction(PteroisBehaviorTreeEnum.HitByFork)
                        .CreateInverter()
                            .CreateSelector()
                                .AddAction(PteroisBehaviorTreeEnum.Struggle)
                                .AddAction(PteroisBehaviorTreeEnum.BeCaught)
                            .EndComposite()
                        .EndDecorator()
                    .EndComposite()
                .EndDecorator()
                .CreateInverter()
                    .CreateSequence()
                        .CreateSelector()
                            .AddAction(PteroisBehaviorTreeEnum.HpLack)
                            .AddAction(PteroisBehaviorTreeEnum.HitByBullet)
                        .EndComposite()
                        .AddAction(PteroisBehaviorTreeEnum.FrightenedSwim)
                    .EndComposite()
                .EndDecorator()
                .CreateInverter()
                    .CreateSequence()
                        .AddAction(PteroisBehaviorTreeEnum.DiscoverPlayer)
                        .CreateInverter()
                            .CreateSequence()
                                .AddAction(PteroisBehaviorTreeEnum.Chase)
                                .AddAction(PteroisBehaviorTreeEnum.Attack)
                            .EndComposite()
                        .EndDecorator()
                    .EndComposite()
                .EndDecorator()
                .AddAction(PteroisBehaviorTreeEnum.Swim);
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}