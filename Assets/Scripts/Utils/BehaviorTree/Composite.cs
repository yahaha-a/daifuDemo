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

        protected Composite()
        {
            ChildLinkedList = new LinkedList<IBehavior>();
        }

        protected virtual bool IfCanAddChildLinkedList()
        {
            return true;
        }

        public void AddChildLinkedList(IBehavior behavior)
        {
            ChildLinkedList.AddLast(behavior);
        }

        public IComposite AddBehavior<TBehavior>(TBehavior composite) where TBehavior : IBehavior
        {
            AddChildLinkedList(composite);
            return this;
        }
    }
}