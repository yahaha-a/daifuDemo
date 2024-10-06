using System.Collections.Generic;
using QFramework;

namespace daifuDemo
{
    public interface IObtainItemsSystem : ISystem
    {
        void AddObtainItemsQueue(IObtainItemsInfo obtainItemsInfo);

        void ShowObtainItem();
    }
    
    public class ObtainItemsSystem : AbstractSystem, IObtainItemsSystem
    {
        private IGameGlobalModel _gameGlobalModel;
        
        protected override void OnInit()
        {
            _gameGlobalModel = this.GetModel<IGameGlobalModel>();

            Events.ObtainItem.Register(obtainItem =>
            {
                AddObtainItemsQueue(obtainItem);
                ShowObtainItem();
            });
        }

        private Queue<IObtainItemsInfo> _obtainItemsQueue = new Queue<IObtainItemsInfo>();

        private Queue<IObtainItemsInfo> _currentObtainItemsQueue = new Queue<IObtainItemsInfo>();

        public void AddObtainItemsQueue(IObtainItemsInfo obtainItemsInfo)
        {
            _obtainItemsQueue.Enqueue(obtainItemsInfo);
        }

        public void ShowObtainItem()
        {
            if (_obtainItemsQueue.Count > 0)
            {
                _gameGlobalModel.CurrentObtainItem.Value = _obtainItemsQueue.Dequeue();
            }
            else
            {
                _gameGlobalModel.CurrentObtainItem.Value = null;
            }
        }
    }
}