using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface ITaskSystem : ISystem
    {
        Dictionary<string, ITaskInfo> TaskNodes { get; }
        
        List<string> CurrentTask { get; }

        ITaskSystem AddTask(string taskKey, ITaskInfo taskInfo);

        ITaskSystem AddDependence(string taskKey, List<string> dependenceTaskKeyList);

        void TraceTask(ITaskInfo taskInfo);

        void RefreshCurrentTask();

        void RefreshTaskGraphState();
    }
    
    public class TaskSystem : AbstractSystem, ITaskSystem
    {
        private ITaskModel _taskModel;

        private ICollectionModel _collectionModel;
        
        protected override void OnInit()
        {
            _taskModel = this.GetModel<ITaskModel>();

            _collectionModel = this.GetModel<ICollectionModel>();
            
            this.AddTask(TaskConfig.CatchOnePteroisKey, new TaskInfo()
                    .WithName("抓一条狮子鱼")
                    .WithDescribe("需要一条狮子鱼")
                    .AddTaskItems(Config.AggressiveFishKey, "狮子鱼", 1)
                    .WithNotStartBehavior(null)
                    .WithIfSatisfyReceiveCondition(() => true)
                    .WithExecuteReceiveCondition(null)
                    .WithExecutingBehavior(() => Events.CatchFish.Register(fish =>
                    {
                        if (fish.FishKey == Config.AggressiveFishKey)
                            TaskNodes[TaskConfig.CatchOnePteroisKey].TaskItems
                                .FirstOrDefault(item => item.Key == Config.AggressiveFishKey)!.CurrentAmount.Value++;
                    }).AddUnRegister(TaskConfig.CatchOnePteroisKey))
                    .WithIfSatisfyCompleteCondition(() => true)
                    .WithExecuteCompleteCondition(() => _collectionModel.Gold.Value++))
                
                .AddTask(TaskConfig.CatchThreeNormalFishKey, new TaskInfo()
                    .WithName("抓三条普通鱼")
                    .WithDescribe("需要三条普通鱼")
                    .AddTaskItems(Config.NormalFishKey, "普通鱼", 3)
                    .WithNotStartBehavior(null)
                    .WithIfSatisfyReceiveCondition(() => true)
                    .WithExecuteReceiveCondition(null)
                    .WithExecutingBehavior(() => Events.CatchFish.Register(fish =>
                    {
                        if (fish.FishKey == Config.NormalFishKey)
                            TaskNodes[TaskConfig.CatchThreeNormalFishKey].TaskItems
                                .FirstOrDefault(item => item.Key == Config.NormalFishKey)!.CurrentAmount.Value++;
                    }).AddUnRegister(TaskConfig.CatchThreeNormalFishKey))
                    .WithIfSatisfyCompleteCondition(() => true)
                    .WithExecuteCompleteCondition(() => _collectionModel.Gold.Value++))
                
                .AddTask(TaskConfig.ReceptionThreeCustomerKey, new TaskInfo()
                    .WithName("接待三位客人")
                    .WithDescribe("需要接待三位客人")
                    .AddTaskItems(Config.CustomerKey, "客人",3)
                    .WithNotStartBehavior(null)
                    .WithIfSatisfyReceiveCondition(() => true)
                    .WithExecuteReceiveCondition(null)
                    .WithExecutingBehavior(() => Events.ReceptionACustomer.Register(() =>
                    {
                        TaskNodes[TaskConfig.ReceptionThreeCustomerKey].TaskItems
                            .FirstOrDefault(item => item.Key == Config.CustomerKey)!.CurrentAmount.Value++;
                    }).AddUnRegister(TaskConfig.ReceptionThreeCustomerKey))
                    .WithIfSatisfyCompleteCondition(() => true)
                    .WithExecuteCompleteCondition(() => _collectionModel.Gold.Value++));

            this.AddDependence(TaskConfig.ReceptionThreeCustomerKey, new List<string>()
            {
                TaskConfig.CatchOnePteroisKey,
                TaskConfig.CatchThreeNormalFishKey
            });

            ActionKit.OnUpdate.Register(() =>
            {
                RefreshCurrentTask();
                RefreshTaskGraphState();
            });

            foreach (var (taskKey, taskInfo) in TaskNodes)
            {
                taskInfo.State.Register(state =>
                {
                    if (state == TaskState.Executing)
                    {
                        taskInfo.ExecutingBehavior?.Invoke();
                    }
                    else if (state == TaskState.Finished)
                    {
                        UtilsExtension.RemoveUnRegister(taskKey);
                    }
                });
            }
        }

        public Dictionary<string, ITaskInfo> TaskNodes { get; } = new Dictionary<string, ITaskInfo>();
        
        public List<string> CurrentTask { get; } = new List<string>();

        public ITaskSystem AddTask(string taskKey, ITaskInfo taskInfo)
        {
            TaskNodes.Add(taskKey, taskInfo);
            return this;
        }

        public ITaskSystem AddDependence(string taskKey, List<string> dependenceTaskKeyList)
        {
            foreach (var dependenceTaskKey in dependenceTaskKeyList)
            {
                if (TaskNodes.ContainsKey(taskKey) && TaskNodes.ContainsKey(dependenceTaskKey))
                {
                    var taskNode = TaskNodes[taskKey];
                    var dependenceTaskNode = TaskNodes[dependenceTaskKey];

                    taskNode.AddDependenceTask(dependenceTaskNode);
                }
            }
            
            return this;
        }
        
        public void TraceTask(ITaskInfo taskInfo)
        {
            _taskModel.CurrentTask.Value = taskInfo;
        }

        public void RefreshCurrentTask()
        {
            foreach (var (taskKey, taskInfo) in TaskNodes)
            {
                if (!CurrentTask.Contains(taskKey) && taskInfo.State.Value == TaskState.Executing)
                {
                    CurrentTask.Add(taskKey);
                }
            }
        }

        public void RefreshTaskGraphState()
        {
            foreach (var (taskKey, taskInfo) in TaskNodes)
            {
                taskInfo.SwitchState();
            }
        }
    }
}