using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine.EventSystems;

namespace daifuDemo
{
    public enum TaskState
    {
        NotStart,
        Executing,
        Finished
    }

    public class TaskItem
    {
        public TaskItem(string key, string name, int targetAmount)
        {
            Key = key;
            Name = name;
            TargetAmount = targetAmount;
        }
        
        public string Key { get; }
        
        public string Name { get; }
        
        public int TargetAmount { get; }

        public BindableProperty<int> CurrentAmount { get; } = new BindableProperty<int>(0);
    }
    
    public interface ITaskInfo
    {
        List<ITaskInfo> DependenceTask { get; }

        List<TaskItem> TaskItems { get; }
        
        string Name { get; }
        
        string Describe { get; }
        
        BindableProperty<TaskState> State { get; }
        
        Action NotStartBehavior { get; }
        
        Func<bool> IfSatisfyReceiveCondition { get; }
        
        Action ExecuteReceiveCondition { get; }
        
        Action ExecutingBehavior { get; }
        
        Func<bool> IfSatisfyCompleteCondition { get; }
        
        Action ExecuteCompleteCondition { get; }

        ITaskInfo WithName(string name);

        ITaskInfo WithDescribe(string describe);

        ITaskInfo WithState(TaskState state);

        ITaskInfo WithNotStartBehavior(Action notStartBehavior);

        ITaskInfo WithIfSatisfyReceiveCondition(Func<bool> ifSatisfyInputCondition);

        ITaskInfo WithExecuteReceiveCondition(Action executeInputCondition);

        ITaskInfo WithExecutingBehavior(Action executingBehavior);

        ITaskInfo WithIfSatisfyCompleteCondition(Func<bool> ifSatisfyOutputCondition);

        ITaskInfo WithExecuteCompleteCondition(Action executeOutputCondition);
        
        ITaskInfo AddDependenceTask(ITaskInfo taskInfo);

        ITaskInfo AddTaskItems(string key, string name, int targetAmount);

        void SwitchState();
    }

    public class TaskInfo : ITaskInfo
    {
        public List<ITaskInfo> DependenceTask { get; } = new List<ITaskInfo>();

        public List<TaskItem> TaskItems { get; } = new List<TaskItem>();

        public string Name { get; private set; }
        
        public string Describe { get; private set; }

        public BindableProperty<TaskState> State { get; private set; } =
            new BindableProperty<TaskState>(TaskState.NotStart);
        
        public Action NotStartBehavior { get; private set; }

        public Func<bool> IfSatisfyReceiveCondition { get; private set; }
        
        public Action ExecuteReceiveCondition { get; private set; }
        
        public Action ExecutingBehavior { get; private set; }

        public Func<bool> IfSatisfyCompleteCondition { get; private set; }
        
        public Action ExecuteCompleteCondition { get; private set; }

        public ITaskInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public ITaskInfo WithDescribe(string describe)
        {
            Describe = describe;
            return this;
        }

        public ITaskInfo WithState(TaskState state)
        {
            State.Value = state;
            return this;
        }

        public ITaskInfo WithNotStartBehavior(Action notStartBehavior)
        {
            NotStartBehavior = notStartBehavior;
            return this;
        }

        public ITaskInfo WithIfSatisfyReceiveCondition(Func<bool> ifSatisfyInputCondition)
        {
            IfSatisfyReceiveCondition = ifSatisfyInputCondition;
            return this;
        }

        public ITaskInfo WithExecuteReceiveCondition(Action executeInputCondition)
        {
            ExecuteReceiveCondition = executeInputCondition;
            return this;
        }

        public ITaskInfo WithExecutingBehavior(Action executingBehavior)
        {
            ExecutingBehavior = executingBehavior;
            return this;
        }

        public ITaskInfo WithIfSatisfyCompleteCondition(Func<bool> ifSatisfyOutputCondition)
        {
            IfSatisfyCompleteCondition = ifSatisfyOutputCondition;
            return this;
        }

        public ITaskInfo WithExecuteCompleteCondition(Action executeOutputCondition)
        {
            ExecuteCompleteCondition = executeOutputCondition;
            return this;
        }
        
        public ITaskInfo AddDependenceTask(ITaskInfo taskInfo)
        {
            DependenceTask.Add(taskInfo);
            return this;
        }

        public ITaskInfo AddTaskItems(string key, string name, int targetAmount)
        {
            TaskItems.Add(new TaskItem(key, name, targetAmount));
            return this;
        }

        public void SwitchState()
        {
            switch (State.Value)
            {
                case TaskState.NotStart:

                    NotStartBehavior?.Invoke();

                    if (DetectionIfCanReceiveTask() && IfSatisfyReceiveCondition?.Invoke() == true)
                    {
                        ExecuteReceiveCondition?.Invoke();
                        State.Value = TaskState.Executing;
                    }
                    break;
                case TaskState.Executing:
                    
                    if (DetectionIfCanCompleteTask() && IfSatisfyCompleteCondition?.Invoke() == true)
                    {
                        ExecuteCompleteCondition?.Invoke();
                        State.Value = TaskState.Finished;
                    }
                    break;
                case TaskState.Finished:
                    break;
            }
        }

        private bool DetectionIfCanReceiveTask()
        {
            foreach (var taskInfo in DependenceTask)
            {
                if (taskInfo.State.Value == TaskState.NotStart || taskInfo.State.Value == TaskState.Executing)
                {
                    return false;
                }
            }

            return true;
        }

        private bool DetectionIfCanCompleteTask()
        {
            foreach (var taskItem in TaskItems)
            {
                if (taskItem.CurrentAmount.Value < taskItem.TargetAmount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}