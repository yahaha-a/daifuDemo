using QFramework;

namespace daifuDemo
{
    public class FindBackPackItemName : AbstractQuery<string>
    {
        private string _backPackItemKey;

        public FindBackPackItemName(string backPackItemKey)
        {
            _backPackItemKey = backPackItemKey;
        }
        
        protected override string OnDo()
        {
            var backPackSystem = this.GetSystem<IBackPackSystem>();
            var itemName = backPackSystem.BackPackItemInfos[_backPackItemKey].ItemName;
            return itemName;
        }
    }
}