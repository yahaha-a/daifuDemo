namespace daifuDemo
{
    public interface ITodayMenuItemInfo
    {
        string Key { get; }
        
        int Amount { get; }

        ITodayMenuItemInfo WithKey(string key);

        ITodayMenuItemInfo WithAmount(int amount);
    }
    
    public class TodayMenuItemInfo : ITodayMenuItemInfo
    {
        public string Key { get; private set; }
        
        public int Amount { get; private set; }
        
        public ITodayMenuItemInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public ITodayMenuItemInfo WithAmount(int amount)
        {
            Amount = amount;
            return this;
        }
    }
}