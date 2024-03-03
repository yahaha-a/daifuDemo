namespace daifuDemo
{
    public enum StaffState
    {
        Free,
        Waiter,
        Cooker
    }
    
    public interface IstaffItemInfo
    {
        string Key { get; }
        
        string Name { get; }
        
        int Rank { get; }
        
        StaffState State { get; }

        IstaffItemInfo WithKey(string key);

        IstaffItemInfo WithName(string name);

        IstaffItemInfo WithRank(int rank);

        IstaffItemInfo WithState(StaffState state);
    }

    public class StaffItemInfo : IstaffItemInfo
    {
        public string Key { get; private set; }
        
        public string Name { get; private set; }
        
        public int Rank { get; private set; }
        
        public StaffState State { get; private set; }
        
        public IstaffItemInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IstaffItemInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IstaffItemInfo WithRank(int rank)
        {
            Rank = rank;
            return this;
        }

        public IstaffItemInfo WithState(StaffState state)
        {
            State = state;
            return this;
        }
    }
}