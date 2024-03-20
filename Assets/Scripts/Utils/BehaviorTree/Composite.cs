using System.Collections.Generic;
using System.Linq;

namespace daifuDemo
{
    public interface IComposite : IBehavior
    {
        IComposite AddBehavior<TBehavior>(TBehavior composite) where TBehavior : IBehavior;
    }
    
    public abstract class Composite : Behavior, IComposite
    {
        protected LinkedList<IBehavior> ChildLinkedList;

        private LinkedList<IBehavior> _childLinkedListCopy;

        protected Composite()
        {
            ChildLinkedList = new LinkedList<IBehavior>();
            _childLinkedListCopy = new LinkedList<IBehavior>();
        }

        protected virtual bool IfCanAddChildLinkedList()
        {
            return true;
        }

        private void AddChildLinkedList(IBehavior behavior)
        {
            ChildLinkedList.AddLast(behavior);
            _childLinkedListCopy.AddLast(behavior);
        }

        public IComposite AddBehavior<TBehavior>(TBehavior composite) where TBehavior : IBehavior
        {
            AddChildLinkedList(composite);
            return this;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            foreach (var behavior in _childLinkedListCopy)
            {
                ChildLinkedList.AddLast(behavior);
            }
        }
    }
}