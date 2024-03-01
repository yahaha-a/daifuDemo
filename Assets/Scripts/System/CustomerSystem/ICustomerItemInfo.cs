using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum CustomerItemState
    {
        Free,
        Walk,
        Order,
        Wait,
        Drink,
        Eat,
        Leave,
        Dead
    }
    
    public interface ICustomerItemInfo
    {
        CustomerItemState State { get; }
        
        BindableProperty<string> CurrentOrderKey { get; }
        
        float WalkSpeed { get; }
        
        Vector2 TargetPosition { get; }
        
        float OrderNeedTime { get; set; }
        
        float WaitTime { get; set; }
        
        float EatTime { get; set; }
        
        float Tip { get; }
        
        bool IfDrink { get; }
        
        float TipMultiple { get; }
        
        bool IfReceiveOrderDish { get; }

        ICustomerItemInfo WithState(CustomerItemState state);

        ICustomerItemInfo WithCurrentOrderKey(string key);

        ICustomerItemInfo WithWalkSpeed(float walkSpeed);

        ICustomerItemInfo WithTargetPosition(Vector2 position);

        ICustomerItemInfo WithOrderNeedTime(float orderNeedTime);

        ICustomerItemInfo WithWaitTime(float waitTime);

        ICustomerItemInfo WithEatTime(float eatTime);

        ICustomerItemInfo WithTip(float tip);

        ICustomerItemInfo WithIfDrink(bool ifDrink);

        ICustomerItemInfo WithTipMultiple(float tipMultiple);

        ICustomerItemInfo WithIfReceiveOrderDish(bool ifReceiveOrderDish);
    }

    public class CustomerItemInfo : ICustomerItemInfo
    {
        public CustomerItemState State { get; private set; }

        public BindableProperty<string> CurrentOrderKey { get; private set; } = new BindableProperty<string>();

        public float WalkSpeed { get; private set; }
        
        public Vector2 TargetPosition { get; private set; }
        
        public float OrderNeedTime { get; set; }

        public float WaitTime { get; set; }

        public float EatTime { get; set; }
        
        public float Tip { get; private set; }
        
        public bool IfDrink { get; private set; }
        
        public float TipMultiple { get; private set; }
        
        public bool IfReceiveOrderDish { get; private set; }

        public ICustomerItemInfo WithState(CustomerItemState state)
        {
            State = state;
            return this;
        }

        public ICustomerItemInfo WithCurrentOrderKey(string key)
        {
            CurrentOrderKey.Value = key;
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

        public ICustomerItemInfo WithOrderNeedTime(float orderNeedTime)
        {
            OrderNeedTime = orderNeedTime;
            return this;
        }

        public ICustomerItemInfo WithWaitTime(float waitTime)
        {
            WaitTime = waitTime;
            return this;
        }

        public ICustomerItemInfo WithEatTime(float eatTime)
        {
            EatTime = eatTime;
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

        public ICustomerItemInfo WithIfReceiveOrderDish(bool ifReceiveOrderDish)
        {
            IfReceiveOrderDish = ifReceiveOrderDish;
            return this;
        }
    }
}