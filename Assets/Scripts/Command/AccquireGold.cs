using QFramework;

namespace daifuDemo
{
    public class AcquireGold : AbstractCommand
    {
        private float _gold;
        
        public AcquireGold(float gold)
        {
            _gold = gold;
        }
        
        protected override void OnExecute()
        {
            var collectionModel = this.GetModel<ICollectionModel>();

            collectionModel.Gold.Value += _gold;
        }
    }
}