using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace daifuDemo
{
    public interface IBehaviorTree<TType>
        where TType : Enum
    {
        IBehaviorTree<TType> AddActionNodeDic(IActionNode<TType> actionNode);
        
        IBehaviorTree<TType> EndComposite();

        IBehaviorTree<TType> EndDecorator();
        
        IBehaviorTree<TType> CreateInverter();

        IBehaviorTree<TType> CreateParallel(int successNeedTime, int failNeedTime);

        IBehaviorTree<TType> CreateRepeat(int maxRepeatTime);

        IBehaviorTree<TType> CreateSelector();

        IBehaviorTree<TType> CreateSequence();

        IBehaviorTree<TType> AddAction(TType type);

        abstract void Init();
        
        void Tick();
    }
    
    public abstract class BehaviorTree<TType> : IBehaviorTree<TType>
        where TType : Enum
    {
        private IBehavior _treeRoot;

        private Stack<IBehavior> _buildStack;

        private Dictionary<TType, IActionNode<TType>> _actionNodeDic;

        protected BehaviorTree()
        {
            _buildStack = new Stack<IBehavior>();
            _actionNodeDic = new Dictionary<TType, IActionNode<TType>>();
        }

        private void BuildBehaviorTree<TBehavior>(TBehavior behavior) where TBehavior : IBehavior
        {
            if (_treeRoot == null)
            {
                _treeRoot = behavior;
                _buildStack.Push(behavior);
            }
            else
            {
                if (_buildStack.Peek() is IComposite)
                {
                    var composite = _buildStack.Peek() as IComposite;
                    composite?.AddBehavior(behavior);
                    _buildStack.Push(behavior);
                }
                else if (_buildStack.Peek() is IDecorator)
                {
                    var decorator = _buildStack.Peek() as IDecorator;
                    decorator?.AddBehavior(behavior);
                    _buildStack.Push(behavior);
                }
            }
        }

        public IBehaviorTree<TType> AddActionNodeDic(IActionNode<TType> actionNode)
        {
            _actionNodeDic.Add(actionNode.ActionType, actionNode);
            return this;
        }

        public IBehaviorTree<TType> EndComposite()
        {
            _buildStack.Pop();
            return this;
        }

        public IBehaviorTree<TType> EndDecorator()
        {
            _buildStack.Pop();
            return this;
        }

        public IBehaviorTree<TType> CreateInverter()
        {
            IDecorator inverter = new Inverter();
            BuildBehaviorTree(inverter);
            
            return this;
        }

        public IBehaviorTree<TType> CreateParallel(int successNeedTime, int failNeedTime)
        {
            Parallel parallel = new Parallel();
            parallel.WithSuccessNeedTime(successNeedTime);
            parallel.WithFailNeedTime(failNeedTime);
            BuildBehaviorTree(parallel);
 
            return this;
        }

        public IBehaviorTree<TType> CreateRepeat(int maxRepeatTime)
        {
            Repeat repeat = new Repeat();
            repeat.WithMaxRepeatTime(maxRepeatTime);
            BuildBehaviorTree(repeat);

            return this;
        }

        public IBehaviorTree<TType> CreateSelector()
        {
            IComposite selector = new Selector();
            BuildBehaviorTree(selector);

            return this;
        }

        public IBehaviorTree<TType> CreateSequence()
        {
            IComposite sequence = new Sequence();
            BuildBehaviorTree(sequence);

            return this;
        }

        public IBehaviorTree<TType> AddAction(TType type)
        {
            BuildBehaviorTree(_actionNodeDic[type]);
            _buildStack.Pop();
            return this;
        }


        public abstract void Init();

        public void Tick()
        {
            _treeRoot.Tick();
        }
    }
}