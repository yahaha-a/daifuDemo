namespace daifuDemo
{
    public enum StaffType
    {
        HeadChef,
        Chef,
        Waiter
    }
    
    public interface IStaffItemInfo
    {
        string Key { get; }
        
        StaffType CurrentType { get; }
        
        string Name { get; }
        
        float CookSpeed { get; }
        
        float WalkSpeed { get; }

        IStaffItemInfo WithKey(string key);

        IStaffItemInfo WithName(string name);

        IStaffItemInfo WithCurrentType(StaffType currentType);

        IStaffItemInfo WithCookSpeed(float cookSpeed);

        IStaffItemInfo WithWalkSpeed(float walkSpeed);
    }

    public class StaffItemInfo : IStaffItemInfo
    {
        public string Key { get; private set; }
        
        public StaffType CurrentType { get; private set; }
        
        public string Name { get; private set; }

        public float CookSpeed { get; private set; }
        
        public float WalkSpeed { get; private set; }

        public IStaffItemInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IStaffItemInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IStaffItemInfo WithCurrentType(StaffType currentType)
        {
            CurrentType = currentType;
            return this;
        }

        public IStaffItemInfo WithCookSpeed(float cookSpeed)
        {
            CookSpeed = cookSpeed;
            return this;
        }

        public IStaffItemInfo WithWalkSpeed(float walkSpeed)
        {
            WalkSpeed = walkSpeed;
            return this;
        }
    }
}