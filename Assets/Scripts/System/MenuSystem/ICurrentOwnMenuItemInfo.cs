namespace daifuDemo
{
    public interface ICurrentOwnMenuItemInfo
    {
        string Key { get; }
        
        bool Unlock { get; }
        
        int Rank { get; }
        
        bool MeetCondition { get; set; }
        
        int CanMakeNumber { get; set; }

        ICurrentOwnMenuItemInfo WithKey(string key);

        ICurrentOwnMenuItemInfo WithUnlock(bool unlock);

        ICurrentOwnMenuItemInfo WithRank(int rank);

        ICurrentOwnMenuItemInfo WithMeetCondition(bool meetCondition);

        ICurrentOwnMenuItemInfo WithCanMakeNumber(int number);
        
        void CheckUnlock();

        void CheckMeetCondition();
    }
    
    public class CurrentOwnMenuItemInfo : ICurrentOwnMenuItemInfo
    {
        public string Key { get; private set; }
        
        public bool Unlock { get; private set; }
        
        public int Rank { get; private set; }
        
        public bool MeetCondition { get; set; }
        
        public int CanMakeNumber { get; set; }

        public ICurrentOwnMenuItemInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public ICurrentOwnMenuItemInfo WithUnlock(bool unlock)
        {
            Unlock = unlock;
            return this;
        }

        public ICurrentOwnMenuItemInfo WithRank(int rank)
        {
            Rank = rank;
            return this;
        }

        public ICurrentOwnMenuItemInfo WithMeetCondition(bool meetCondition)
        {
            MeetCondition = meetCondition;
            return this;
        }

        public ICurrentOwnMenuItemInfo WithCanMakeNumber(int number)
        {
            CanMakeNumber = number;
            return this;
        }

        public void CheckUnlock()
        {
            
        }

        public void CheckMeetCondition()
        {
            
        }
    }
}