namespace daifuDemo
{
    public interface IDecorator : IBehavior
    {
        IDecorator AddBehavior<TBehavior>(TBehavior composite) where TBehavior : IBehavior;
    }
    
    public abstract class Decorator : Behavior, IDecorator
    {
        protected IBehavior ChildNode;

        public void WithChildNode(IBehavior childNode)
        {
            ChildNode = childNode;
        }
        
        public IDecorator AddBehavior<TBehavior>(TBehavior composite) where TBehavior : IBehavior
        {
            WithChildNode(composite);
            return this;
        }
    }
}