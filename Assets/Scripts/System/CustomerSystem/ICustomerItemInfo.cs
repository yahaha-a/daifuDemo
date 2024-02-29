using UnityEngine;

namespace daifuDemo
{
    public enum CustomerItemState
    {
        Free,
        Walk,
        Drink,
        Eat,
        Dead
    }
    
    public interface ICustomerItemInfo
    {
        CustomerItemState State { get; }
        
        float WalkSpeed { get; }
        
        Vector2 TargetPosition { get; }
        
        float WaitTime { get; }
        
        float Tip { get; }
        
        bool IfDrink { get; }
        
        float TipMultiple { get; }

        ICustomerItemInfo WithState(CustomerItemState state);

        ICustomerItemInfo WithWalkSpeed(float walkSpeed);

        ICustomerItemInfo WithTargetPosition(Vector2 position);

        ICustomerItemInfo WithWaitTime(float waitTime);

        ICustomerItemInfo WithTip(float tip);

        ICustomerItemInfo WithIfDrink(bool ifDrink);

        ICustomerItemInfo WithTipMultiple(float tipMultiple);
    }

    public class CustomerItemInfo : ICustomerItemInfo
    {
        public CustomerItemState State { get; private set; }
        
        public float WalkSpeed { get; private set; }
        
        public Vector2 TargetPosition { get; private set; }

        public float WaitTime { get; private set; }
        
        public float Tip { get; private set; }
        
        public bool IfDrink { get; private set; }
        
        public float TipMultiple { get; private set; }

        public ICustomerItemInfo WithState(CustomerItemState state)
        {
            State = state;
            return this;
        }

        public ICustomerItemInfo WithWalkSpeed(float walkSpeed)
        {
            WalkSpeed = walkSpeed;
            return this;
        }

        public ICustomerItemInfo WithTargetPosition(Vector2 position)
        {
            TargetPosition = position;
            return this;
        }

        public ICustomerItemInfo WithWaitTime(float waitTime)
        {
            WaitTime = waitTime;
            return this;
        }

        public ICustomerItemInfo WithTip(float tip)
        {
            Tip = tip;
            return this;
        }

        public ICustomerItemInfo WithIfDrink(bool ifDrink)
        {
            IfDrink = ifDrink;
            return this;
        }

        public ICustomerItemInfo WithTipMultiple(float tipMultiple)
        {
            TipMultiple = tipMultiple;
            return this;
        }
    }
}