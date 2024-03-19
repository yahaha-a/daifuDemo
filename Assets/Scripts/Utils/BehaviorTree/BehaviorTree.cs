using System.Collections.Generic;
using Unity.VisualScripting;

namespace daifuDemo
{
    public interface IBehaviorTree<TType>
    {
        IBehaviorTree<TType> EndComposite();

        IBehaviorTree<TType> EndDecorator();
        
        IBehaviorTree<TType> CreateInverter();

        IBehaviorTree<TType> CreateParallel();

        IBehaviorTree<TType> CreateRepeat();

        IBehaviorTree<TType> CreateSelector();

        IBehaviorTree<TType> CreateSequence();

        IBehaviorTree<TType> AddActionNode(TType type);

        abstract void Init();
        
        void Tick();
    }
    
    public abstract class BehaviorTree<TType> : IBehaviorTree<TType>
    {
        private IBehavior _treeRoot;

        private Stack<IBehavior> _buildStack;

        protected Dictionary<TType, IActionNode> ActionNodeDic;

        protected BehaviorTree()
        {
            _buildStack = new Stack<IBehavior>();
            ActionNodeDic = new Dictionary<TType, IActionNode>();
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
                while (_buildStack.Peek() is ActionNode)
                {
                    _buildStack.Pop();
                }

                if (_buildStack.Peek() is Composite)
                {
                    var composite = _buildStack.Peek() as Composite;
                    composite?.AddBehavior(behavior);
                    _buildStack.Push(behavior);
                }
                
                if (_buildStack.Peek() is Decorator)
                {
                    var decorator = _buildStack.Peek() as Decorator;
                    decorator?.AddBehavior(behavior);
                    _buildStack.Pop();
                    _buildStack.Push(behavior);
                }
            }
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

        public IBehaviorTree<TType> CreateParallel()
        {
            IComposite parallel = new Parallel();
            BuildBehaviorTree(parallel);
 
            return this;
        }

        public IBehaviorTree<TType> CreateRepeat()
        {
            IDecorator repeat = new Repeat();
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

        public IBehaviorTree<TType> AddActionNode(TType type)
        {
            BuildBehaviorTree(ActionNodeDic[type]);
            return this;
        }


        public abstract void Init();

        public void Tick()
        {
            _treeRoot.Tick();
        }
    }
}