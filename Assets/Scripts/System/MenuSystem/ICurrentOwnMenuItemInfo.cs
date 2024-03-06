using QFramework;

namespace daifuDemo
{
    public interface ICurrentOwnMenuItemInfo
    {
        BindableProperty<string> Key { get; }
        
        BindableProperty<bool> Unlock { get; }
        
        BindableProperty<int> Rank { get; }
        
        BindableProperty<bool> MeetCondition { get; }
        
        BindableProperty<int> CanMakeNumber { get; }

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
        public BindableProperty<string> Key { get; private set; } = new BindableProperty<string>(null);

        public BindableProperty<bool> Unlock { get; private set; } = new BindableProperty<bool>(false);

        public BindableProperty<int> Rank { get; private set; } = new BindableProperty<int>(0);

        public BindableProperty<bool> MeetCondition { get; private set; } = new BindableProperty<bool>(false);

        public BindableProperty<int> CanMakeNumber { get; private set; } = new BindableProperty<int>(0);

        public ICurrentOwnMenuItemInfo WithKey(string key)
        {
            Key.Value = key;
            return this;
        }

        public ICurrentOwnMenuItemInfo WithUnlock(bool unlock)
        {
            Unlock.Value = unlock;
            return this;
        }

        public ICurrentOwnMenuItemInfo WithRank(int rank)
        {
            Rank.Value = rank;
            return this;
        }

        public ICurrentOwnMenuItemInfo WithMeetCondition(bool meetCondition)
        {
            MeetCondition.Value = meetCondition;
            return this;
        }

        public ICurrentOwnMenuItemInfo WithCanMakeNumber(int number)
        {
            CanMakeNumber.Value = number;
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