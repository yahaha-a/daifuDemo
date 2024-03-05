using QFramework;

namespace daifuDemo
{
    public interface ITodayMenuItemInfo
    {
        int Node { get; set; }
        
        BindableProperty<string> Key { get; set; }
        
        BindableProperty<int> Amount { get; set; }

        ITodayMenuItemInfo WithNode(int node);
        
        ITodayMenuItemInfo WithKey(string key);

        ITodayMenuItemInfo WithAmount(int amount);
    }
    
    public class TodayMenuItemInfo : ITodayMenuItemInfo
    {
        public int Node { get; set; }
        public BindableProperty<string> Key { get; set; } = new BindableProperty<string>(null);

        public BindableProperty<int> Amount { get; set; } = new BindableProperty<int>(0);

        public ITodayMenuItemInfo WithNode(int node)
        {
            Node = node;
            return this;
        }

        public ITodayMenuItemInfo WithKey(string key)
        {
            Key.Value = key;
            return this;
        }

        public ITodayMenuItemInfo WithAmount(int amount)
        {
            Amount.Value = amount;
            return this;
        }
    }
}