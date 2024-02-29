namespace daifuDemo
{
    public enum CustomerType
    {
        Woman,
        Child,
        Man,
        OldMan
    }
    
    public interface ICustomerInfo
    {
        float MaxWaitTime { get; }
        
        float MinWaitTime { get; }
        
        float WalkSpeed { get; }
        
        float MaxTip { get; }
        
        float MinTip { get; }
        
        float MaxEatTime { get; }
        
        float MinEatTime { get; }
        
        float DrinkProbability { get; }
        
        float DrinkTipMultiple { get; }

        ICustomerInfo WithMaxWaitTime(float maxWaitTime);

        ICustomerInfo WithMinWaitTime(float minWaitTime);

        ICustomerInfo WithWalkSpeed(float walkSpeed);

        ICustomerInfo WithMaxTip(float maxTip);

        ICustomerInfo WithMinTip(float minTip);

        ICustomerInfo WithMaxEatTime(float maxEatTime);

        ICustomerInfo WithMinEatTime(float minEatTime);

        ICustomerInfo WithDrinkProbability(float drinkProbability);

        ICustomerInfo WithDrinkTipMultiple(float drinkTipMultiple);
    }

    public class CustomerInfo : ICustomerInfo
    {
        public float MaxWaitTime { get; private set; }
        
        public float MinWaitTime { get; private set; }
        
        public float WalkSpeed { get; private set; }
        
        public float MaxTip { get; private set; }
        
        public float MinTip { get; private set; }
        
        public float MaxEatTime { get; private set; }
        
        public float MinEatTime { get; private set; }

        public float DrinkProbability { get; private set; }
        
        public float DrinkTipMultiple { get; private set; }
        
        public ICustomerInfo WithMaxWaitTime(float maxWaitTime)
        {
            MaxWaitTime = maxWaitTime;
            return this;
        }

        public ICustomerInfo WithMinWaitTime(float minWaitTime)
        {
            MinWaitTime = minWaitTime;
            return this;
        }

        public ICustomerInfo WithWalkSpeed(float walkSpeed)
        {
            WalkSpeed = walkSpeed;
            return this;
        }

        public ICustomerInfo WithMaxTip(float maxTip)
        {
            MaxTip = maxTip;
            return this;
        }

        public ICustomerInfo WithMinTip(float minTip)
        {
            MinTip = minTip;
            return this;
        }

        public ICustomerInfo WithMaxEatTime(float maxEatTime)
        {
            MaxEatTime = maxEatTime;
            return this;
        }

        public ICustomerInfo WithMinEatTime(float minEatTime)
        {
            MinEatTime = minEatTime;
            return this;
        }

        public ICustomerInfo WithDrinkProbability(float drinkProbability)
        {
            DrinkProbability = drinkProbability;
            return this;
        }

        public ICustomerInfo WithDrinkTipMultiple(float drinkTipMultiple)
        {
            DrinkTipMultiple = drinkTipMultiple;
            return this;
        }
    }
}