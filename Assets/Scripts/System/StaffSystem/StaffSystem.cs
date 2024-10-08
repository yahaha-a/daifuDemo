using System.Collections.Generic;
using System.Linq;
using QFramework;

namespace daifuDemo
{
    public interface IStaffSystem : ISystem
    {
        Dictionary<string, IStaffInfo> StaffItemInfos { get; }
        
        Dictionary<string, IstaffItemInfo> CurrentOwnStaffItems { get; }
        
        LinkedList<(int, string)> CurrentCookers { get; }
        
        LinkedList<(int, string)> CurrentWaiters { get; }
        
        IStaffSystem AddStaffItemInfos(string key, IStaffInfo staffInfo);

        void AddCurrentOwnStaffItems(string key, IstaffItemInfo staffItemInfo);

        void AddCooker(int node, string key);

        void AddWaiter(int node, string key);
    }
    
    public class StaffSystem : AbstractSystem, IStaffSystem
    {
        protected override void OnInit()
        {
            //TODO
            this.AddStaffItemInfos(StaffConfig.AaaKey, new StaffInfo()
                    .WithKey(StaffConfig.AaaKey)
                    .WithName("Aaa")
                    .WithRankWithWalkSpeed(new List<(int, float)>()
                    {
                        (1, 5), (2, 6), (3, 7)
                    })
                    .WithRankWithCookSpeed(new List<(int, float)>()
                    {
                        (1, 1.1f), (2, 1.2f), (3, 1.3f)
                    }))
                .AddStaffItemInfos(StaffConfig.BbbKey, new StaffInfo()
                    .WithKey(StaffConfig.BbbKey)
                    .WithName("Bbb")
                    .WithRankWithWalkSpeed(new List<(int, float)>()
                    {
                        (1, 4), (2, 5), (3, 6)
                    })
                    .WithRankWithCookSpeed(new List<(int, float)>()
                    {
                        (1, 1.13f), (2, 1.25f), (3, 1.4f)
                    }))
                .AddStaffItemInfos(StaffConfig.CccKey, new StaffInfo()
                    .WithKey(StaffConfig.CccKey)
                    .WithName("Ccc")
                    .WithRankWithWalkSpeed(new List<(int, float)>()
                    {
                        (1, 3.5f), (2, 3.6f), (3, 3.7f)
                    })
                    .WithRankWithCookSpeed(new List<(int, float)>()
                    {
                        (1, 1.2f), (2, 1.4f), (3, 1.6f)
                    }))
                .AddStaffItemInfos(StaffConfig.DddKey, new StaffInfo()
                    .WithKey(StaffConfig.DddKey)
                    .WithName("Ddd")
                    .WithRankWithWalkSpeed(new List<(int, float)>()
                    {
                        (1, 2.4f), (2, 2.5f), (3, 2.6f)
                    })
                    .WithRankWithCookSpeed(new List<(int, float)>()
                    {
                        (1, 1.3f), (2, 1.6f), (3, 1.8f)
                    }));
            
            this.AddCurrentOwnStaffItems(StaffConfig.AaaKey, new StaffItemInfo()
                .WithKey(StaffConfig.AaaKey)
                .WithName(StaffItemInfos[StaffConfig.AaaKey].Name)
                .WithRank(1)
                .WithState(StaffState.Free));
            
            this.AddCurrentOwnStaffItems(StaffConfig.BbbKey, new StaffItemInfo()
                .WithKey(StaffConfig.BbbKey)
                .WithName(StaffItemInfos[StaffConfig.BbbKey].Name)
                .WithRank(1)
                .WithState(StaffState.Free));
            
            this.AddCurrentOwnStaffItems(StaffConfig.CccKey, new StaffItemInfo()
                .WithKey(StaffConfig.CccKey)
                .WithName(StaffItemInfos[StaffConfig.CccKey].Name)
                .WithRank(1)
                .WithState(StaffState.Free));
            
            this.AddCurrentOwnStaffItems(StaffConfig.DddKey, new StaffItemInfo()
                .WithKey(StaffConfig.DddKey)
                .WithName(StaffItemInfos[StaffConfig.DddKey].Name)
                .WithRank(1)
                .WithState(StaffState.Free));

            for (int i = 1; i <= 2; i++)
            {
                CurrentCookers.AddLast((i, null));
                CurrentWaiters.AddLast((i, null));
            }
        }

        public Dictionary<string, IStaffInfo> StaffItemInfos { get; } = new Dictionary<string, IStaffInfo>();

        public Dictionary<string, IstaffItemInfo> CurrentOwnStaffItems { get; } =
            new Dictionary<string, IstaffItemInfo>();

        public LinkedList<(int, string)> CurrentCookers { get; } =
            new LinkedList<(int, string)>();

        public LinkedList<(int, string)> CurrentWaiters { get; } =
            new LinkedList<(int, string)>();

        public IStaffSystem AddStaffItemInfos(string key, IStaffInfo staffInfo)
        {
            StaffItemInfos.Add(key, staffInfo);
            return this;
        }

        public void AddCurrentOwnStaffItems(string key, IstaffItemInfo staffItemInfo)
        {
            CurrentOwnStaffItems.Add(key, staffItemInfo);
        }

        public void AddCooker(int node, string key)
        {
            LinkedListNode<(int, string)> listNode =
                CurrentCookers.Find(CurrentCookers.FirstOrDefault(item => item.Item1 == node));

            if (listNode != null)
            {
                listNode.Value = new(node, key);
                CurrentOwnStaffItems[key].WithState(StaffState.Cooker);
            }
        }

        public void AddWaiter(int node, string key)
        {
            LinkedListNode<(int, string)> listNode =
                CurrentWaiters.Find(CurrentWaiters.FirstOrDefault(item => item.Item1 == node));

            if (listNode != null)
            {
                listNode.Value = new(node, key);
                CurrentOwnStaffItems[key].WithState(StaffState.Waiter);
            }
        }
    }
}