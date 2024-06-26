using QFramework;
using UnityEngine;

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
        
        Player Player { get; set; }
    }
    
    public class NormalFishBehaviorTree<TFish> : BehaviorTree<NormalFishBehaviorTreeEnum>, IFishBehaviorTree<TFish> where TFish : NormalFish
    {
        public TFish Fish { get; set; }
        
        public Player Player { get; set; }
        
        public NormalFishBehaviorTree(TFish fish, Player player)
        {
            Fish = fish;
            Player = player;
        }
        
        public override void Init()
        {
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
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.Struggle)
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
                    .WithOnSuccessExit(() => {})
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
                    .WithOnSuccessExit(() => {})
                    .WithOnFailExit(() => {}))

                .AddActionNodeDic(new ActionNode<NormalFishBehaviorTreeEnum>()
                    .WithActionType(NormalFishBehaviorTreeEnum.FrightenedSwim)
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
    }
}