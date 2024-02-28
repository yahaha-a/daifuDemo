namespace daifuDemo
{
    public interface ITodayMenuItemInfo
    {
        int Node { get; set; }
        
        string Key { get; set; }
        
        int Amount { get; set; }

        ITodayMenuItemInfo WithNode(int node);
        
        ITodayMenuItemInfo WithKey(string key);

        ITodayMenuItemInfo WithAmount(int amount);
    }
    
    public class TodayMenuItemInfo : ITodayMenuItemInfo
    {
        public int Node { get; set; }
        public string Key { get; set; }
        
        public int Amount { get; set; }

        public ITodayMenuItemInfo WithNode(int node)
        {
            Node = node;
            return this;
        }

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