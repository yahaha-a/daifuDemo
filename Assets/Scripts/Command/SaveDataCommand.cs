using QFramework;

namespace daifuDemo
{
    public class SaveDataCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var collectionModel = this.GetModel<ICollectionModel>();
            
            collectionModel.Storage();
        }
    }
}