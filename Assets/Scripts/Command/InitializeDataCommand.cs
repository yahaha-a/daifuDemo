using QFramework;

namespace daifuDemo
{
    public class InitializeDataCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var collectionModel = this.GetModel<ICollectionModel>();
            
            collectionModel.Load();
        }
    }
}