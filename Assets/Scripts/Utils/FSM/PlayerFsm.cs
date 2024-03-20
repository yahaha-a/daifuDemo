using System;
using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum PlayStateEnum
    {
        Normal,
        Special
    }
    
    public enum PlayNormalStateEnum
    {
        Idle,
        Walk,
        Run
    }
    
    public enum PlaySpecialStateEnum
    {
        Idle,
        Walk,
        Run
    }
    
    public class PlayerFsm : AbstractFsm<PlayStateEnum>
    {
        private PlayerNormalFsm _playerNormalFsm;
        private PlayerSpecialFsm _playerSpecialFsm;
        
        public override void Init()
        {
            _playerNormalFsm = new PlayerNormalFsm();
            _playerSpecialFsm = new PlayerSpecialFsm();
            
            AddStates(new State<PlayStateEnum>()
                .WithKey(PlayStateEnum.Normal)
                .WithOnEnter(() => Debug.Log("进入普通状态"))
                .WithOnExit(() => Debug.Log("离开普通状态"))
                .WithOnTick(() => _playerNormalFsm.Tick()));
            AddStates(new State<PlayStateEnum>()
                .WithKey(PlayStateEnum.Special)
                .WithOnEnter(() => Debug.Log("进入特殊状态"))
                .WithOnExit(() => Debug.Log("离开特殊状态"))
                .WithOnTick(() => _playerSpecialFsm.Tick()));

            AddTransitions(new TriggerTransition<PlayStateEnum>()
                .WithFromState(StateDic[PlayStateEnum.Normal])
                .WithToState(StateDic[PlayStateEnum.Special])
                .WithWeight(1)
                .AddConditions(new TriggerCondition()
                    .WithCondition(TriggerConditionEnum.Repeatedly)
                    .WithCurrentValue(() => () => Input.GetKeyDown(KeyCode.Q))
                    .WithTargetValue(null)));
            AddTransitions(new TriggerTransition<PlayStateEnum>()
                .WithFromState(StateDic[PlayStateEnum.Special])
                .WithToState(StateDic[PlayStateEnum.Normal])
                .WithWeight(1)
                .AddConditions(new TriggerCondition()
                    .WithCondition(TriggerConditionEnum.Repeatedly)
                    .WithCurrentValue(() => () => Input.GetKeyDown(KeyCode.Q))
                    .WithTargetValue(null)));

            WithIdleState(StateDic[PlayStateEnum.Normal]);
        }
    }


    public class PlayerNormalFsm : AbstractFsm<PlayNormalStateEnum>
    {
        public override void Init()
        {
            AddStates(new State<PlayNormalStateEnum>()
                .WithKey(PlayNormalStateEnum.Idle)
                .WithOnEnter(() => Debug.Log("开始普通站立"))
                .WithOnExit(() => Debug.Log("结束普通站立"))
                .WithOnTick(null));
            AddStates(new State<PlayNormalStateEnum>()
                .WithKey(PlayNormalStateEnum.Walk)
                .WithOnEnter(() => Debug.Log("开始普通走"))
                .WithOnExit(() => Debug.Log("结束普通走"))
                .WithOnTick(null));
            
            AddTransitions(new TriggerTransition<PlayNormalStateEnum>()
                .WithFromState(StateDic[PlayNormalStateEnum.Idle])
                .WithToState(StateDic[PlayNormalStateEnum.Walk])
                .WithWeight(1)
                .AddConditions(new TriggerCondition()
                    .WithCondition(TriggerConditionEnum.Repeatedly)
                    .WithCurrentValue(() => () => Input.GetKeyDown(KeyCode.W))
                    .WithTargetValue(null)));
            AddTransitions(new TriggerTransition<PlayNormalStateEnum>()
                .WithFromState(StateDic[PlayNormalStateEnum.Walk])
                .WithToState(StateDic[PlayNormalStateEnum.Idle])
                .WithWeight(1)
                .AddConditions(new TriggerCondition()
                    .WithCondition(TriggerConditionEnum.Repeatedly)
                    .WithCurrentValue(() => () => Input.GetKeyDown(KeyCode.S))
                    .WithTargetValue(null)));
            
            WithIdleState(StateDic[PlayNormalStateEnum.Idle]);
        }
    }
    
    public class PlayerSpecialFsm : AbstractFsm<PlaySpecialStateEnum>
    {
        public override void Init()
        {
            AddStates(new State<PlaySpecialStateEnum>()
                .WithKey(PlaySpecialStateEnum.Idle)
                .WithOnEnter(() => Debug.Log("开始特殊站立"))
                .WithOnExit(() => Debug.Log("离开特殊站立"))
                .WithOnTick(null));
            AddStates(new State<PlaySpecialStateEnum>()
                .WithKey(PlaySpecialStateEnum.Walk)
                .WithOnEnter(() => Debug.Log("开始特殊走"))
                .WithOnExit(() => Debug.Log("结束特殊走"))
                .WithOnTick(null));
            
            AddTransitions(new TriggerTransition<PlaySpecialStateEnum>()
                .WithFromState(StateDic[PlaySpecialStateEnum.Idle])
                .WithToState(StateDic[PlaySpecialStateEnum.Walk])
                .WithWeight(1)
                .AddConditions(new TriggerCondition()
                    .WithCondition(TriggerConditionEnum.Repeatedly)
                    .WithCurrentValue(() => () => Input.GetKeyDown(KeyCode.W))
                    .WithTargetValue(null)));
            AddTransitions(new TriggerTransition<PlaySpecialStateEnum>()
                .WithFromState(StateDic[PlaySpecialStateEnum.Walk])
                .WithToState(StateDic[PlaySpecialStateEnum.Idle])
                .WithWeight(1)
                .AddConditions(new TriggerCondition()
                    .WithCondition(TriggerConditionEnum.Repeatedly)
                    .WithCurrentValue(() => () => Input.GetKeyDown(KeyCode.S))
                    .WithTargetValue(null)));

            WithIdleState(StateDic[PlaySpecialStateEnum.Idle]);
        }
    }
}