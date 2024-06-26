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
    
    public class PteroisFishBehaviorTree<TFish> : BehaviorTree<PteroisBehaviorTreeEnum>, IFishBehaviorTree<TFish> where TFish : Pterois
    {
        public TFish Fish { get; set; }
        public Player Player { get; set; }
        
        public PteroisFishBehaviorTree(TFish fish, Player player)
        {
            Fish = fish;
            Player = player;
        }
        
        public override void Init()
        {
            this
                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.HpZero)
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

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Die)
                    .WithOnUpdate(null)
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
                            Fish.CanSwim = false;
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Struggle)
                    .WithOnUpdate(() =>
                    {
                        Fish.CurrentStruggleTime += Time.deltaTime;

                        if (Fish.CurrentStruggleTime >= Fish.StruggleTime)
                        {
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() =>
                    {
                        Events.FishEscape?.Trigger(Fish);
                        Fish.HitByFork = false;
                        Fish.CurrentStruggleTime = 0;
                        Player.ResetFishChallengeClicks();
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.BeCaught)
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
                                Fish.transform.position = Vector3.Lerp(position, Player.transform.position,
                                    1 - Mathf.Exp(-Time.deltaTime * 30));
                            }
                        }).UnRegisterWhenGameObjectDestroyed(Fish.gameObject);
                    })
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Swim)
                    .WithOnUpdate(() =>
                    {
                        Fish.CanSwim = true;

                        if (Vector3.Distance(Fish.transform.position, Fish.StartPosition) >= Fish.RangeOfMovement)
                        {
                            Fish.CurrentDirection = -Fish.CurrentDirection;
                        }

                        Fish.CurrentSwimRate = Fish.SwimRate;
                        Fish.CurrentToggleDirectionTime -= Time.deltaTime;
                        if (Fish.CurrentToggleDirectionTime <= 0)
                        {
                            Fish.CurrentToggleDirectionTime = Fish.ToggleDirectionTime;
                            Fish.CurrentDirection =
                                new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                        }

                        return BehaviorNodeState.Success;
                    })
                    .WithOnSuccessExit(() => {})
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
                    .WithOnSuccessExit(() => {})
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
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.FrightenedSwim)
                    .WithOnUpdate(() =>
                    {
                        if (Vector3.Distance(Fish.transform.position, Fish.StartPosition) >= Fish.RangeOfMovement)
                        {
                            Fish.CurrentDirection = -Fish.CurrentDirection;
                        }

                        Fish.CurrentDirection = (Fish.transform.position - Player.transform.position).normalized;
                        Fish.CurrentSwimRate = Fish.FrightenedSwimRate;

                        return BehaviorNodeState.Success;
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Chase)
                    .WithOnUpdate(() =>
                    {
                        if (Vector3.Distance(Player.transform.position, Fish.transform.position) > Fish.AttackRange)
                        {
                            Fish.CurrentDirection = (Player.transform.position - Fish.transform.position).normalized;
                            Fish.CurrentSwimRate = Fish.PursuitSwimRate;
                            
                            return BehaviorNodeState.Success;
                        }

                        return BehaviorNodeState.Fail;
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<PteroisBehaviorTreeEnum>()
                    .WithActionType(PteroisBehaviorTreeEnum.Attack)
                    .WithOnUpdate(() =>
                    {
                        if (Fish.CurrentAttackInterval <= 0f)
                        {
                            Fish.SendCommand(new PlayerIsHitCommand(Fish.Damage));
					
                            Fish.CurrentAttackInterval = Fish.AttackInterval;
                        }
                        else
                        {
                            Fish.CurrentAttackInterval -= Time.deltaTime;
                        }
                        
                        return BehaviorNodeState.Success;
                    })
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() =>
                    {
                        
                    }))

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
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}));
            
            CreateSequence()
                .CreateInverter()
                    .CreateSelector()
                        .CreateSequence()
                            .AddAction(PteroisBehaviorTreeEnum.HpZero)
                            .AddAction(PteroisBehaviorTreeEnum.Die)
                        .EndComposite()
                        .CreateSequence()
                            .AddAction(PteroisBehaviorTreeEnum.HitByFork)
                            .CreateInverter()
                                .CreateSelector()
                                    .AddAction(PteroisBehaviorTreeEnum.Struggle)
                                    .AddAction(PteroisBehaviorTreeEnum.BeCaught)
                                .EndComposite()
                            .EndDecorator()
                        .EndComposite()
                    .EndComposite()
                .EndDecorator()
                .CreateSequence()
                    .AddAction(PteroisBehaviorTreeEnum.Swim)
                    .CreateSequence()
                        .CreateSequence()
                            .AddAction(PteroisBehaviorTreeEnum.DiscoverPlayer)
                            .CreateSelector()
                                .AddAction(PteroisBehaviorTreeEnum.Chase)
                                .AddAction(PteroisBehaviorTreeEnum.Attack)
                            .EndComposite()
                        .EndComposite()
                        .CreateSequence()
                            .AddAction(PteroisBehaviorTreeEnum.HpLack)
                            .AddAction(PteroisBehaviorTreeEnum.FrightenedSwim)
                        .EndComposite()
                    .EndComposite()
                .EndComposite()
            .EndComposite();
        }
    }
}