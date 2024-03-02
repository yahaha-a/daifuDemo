using System.Collections.Generic;
using QFramework;

namespace daifuDemo
{
    public interface IStaffSystem : ISystem
    {
        Dictionary<string, IStaffItemInfo> StaffItemInfos { get; }
        
        BindableProperty<Dictionary<string, IStaffItemInfo>> CurrentCookerItems { get; }
        
        BindableProperty<Dictionary<string, IStaffItemInfo>> CurrentWaiterItems { get; }

        IStaffSystem AddStaffItemInfos(string key, IStaffItemInfo staffItemInfo);

        void AddCurrentCookerItem(string key, IStaffItemInfo cookerItemInfo);

        void AddCurrentWaiterItem(string key, IStaffItemInfo waiterItemInfo);
    }
    
    public class StaffSystem : AbstractSystem, IStaffSystem
    {
        protected override void OnInit()
        {
            CurrentCookerItems.Value = new Dictionary<string, IStaffItemInfo>();
            CurrentWaiterItems.Value = new Dictionary<string, IStaffItemInfo>();
            
            this.AddStaffItemInfos(StaffConfig.AaaKey, new StaffItemInfo()
                    .WithKey(StaffConfig.AaaKey)
                    .WithName("Aaa")
                    .WithWalkSpeed(4f)
                    .WithCookSpeed(0.8f))
                .AddStaffItemInfos(StaffConfig.BbbKey, new StaffItemInfo()
                    .WithKey(StaffConfig.BbbKey)
                    .WithName("Bbb")
                    .WithWalkSpeed(5f)
                    .WithCookSpeed(1.2f));
            
            this.AddCurrentCookerItem(StaffConfig.AaaKey, new StaffItemInfo()
                .WithKey(StaffConfig.AaaKey)
                .WithName("Aaa")
                .WithWalkSpeed(4f)
                .WithCookSpeed(1.1f));
        }

        public Dictionary<string, IStaffItemInfo> StaffItemInfos { get; } = new Dictionary<string, IStaffItemInfo>();

        public BindableProperty<Dictionary<string, IStaffItemInfo>> CurrentCookerItems { get; } =
            new BindableProperty<Dictionary<string, IStaffItemInfo>>();

        public BindableProperty<Dictionary<string, IStaffItemInfo>> CurrentWaiterItems { get; } =
            new BindableProperty<Dictionary<string, IStaffItemInfo>>();

        public IStaffSystem AddStaffItemInfos(string key, IStaffItemInfo staffItemInfo)
        {
            StaffItemInfos.Add(key, staffItemInfo);
            return this;
        }

        public void AddCurrentCookerItem(string key, IStaffItemInfo cookerItemInfo)
        {
            CurrentCookerItems.Value.Add(key, cookerItemInfo);
        }

        public void AddCurrentWaiterItem(string key, IStaffItemInfo waiterItemInfo)
        {
            CurrentWaiterItems.Value.Add(key, waiterItemInfo);
        }
    }
}