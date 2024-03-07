using QFramework;

namespace daifuDemo
{
    public enum AchievementState
    {
        Process,
        Finished
    }
    
    public interface IAchievementItemInfo
    {
        string Key { get; set; }
        
        string CompleteTime { get; set; }
        
        BindableProperty<AchievementState> CurrentAchievementState { get; set; }

        IAchievementItemInfo WithKey(string key);

        IAchievementItemInfo WithCompleteTime(string completeTime);

        IAchievementItemInfo WithCurrentAchievementState(AchievementState state);
    }

    public class AchievementItemInfo : IAchievementItemInfo
    {
        public string Key { get; set; }
        
        public string CompleteTime { get; set; }

        public BindableProperty<AchievementState> CurrentAchievementState { get; set; } =
            new BindableProperty<AchievementState>(AchievementState.Process);

        public IAchievementItemInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IAchievementItemInfo WithCompleteTime(string completeTime)
        {
            CompleteTime = completeTime;
            return this;
        }

        public IAchievementItemInfo WithCurrentAchievementState(AchievementState state)
        {
            CurrentAchievementState.Value = state;
            return this;
        }
    }
}