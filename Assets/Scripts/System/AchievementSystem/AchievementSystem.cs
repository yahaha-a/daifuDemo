using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IAchievementSystem : ISystem
    {
        Dictionary<string, IAchievementInfo> AchievementInfos { get; }
        
        Dictionary<string, IAchievementItemInfo> AchievementItems { get; }

        string IfHaveAchievementCanComplete();

        void SettleAchievementAward(string key);
    }
    
    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        private ICollectionModel _collectionModel;

        private IAchievementModel _achievementModel;
        
        protected override void OnInit()
        {
            _collectionModel = this.GetModel<ICollectionModel>();
            _achievementModel = this.GetModel<IAchievementModel>();

            this.AddAchievementSystem(AchievementConfig.ReceptionTenCustomersKey, new AchievementInfo()
                    .WithName("新店开张")
                    .WithIcon(null)
                    .WithDescription("首次接待十名客人")
                    .WithIfComplete(() => _collectionModel.ReceptionCustomerTotalAmount.Value == 10)
                    .WithAward(() => _collectionModel.Gold.Value += 1))
                .AddAchievementSystem(AchievementConfig.SellTenNormalFishsushiKey, new AchievementInfo()
                    .WithName("第十条鱼")
                    .WithIcon(null)
                    .WithDescription("卖出十份普通鱼寿司")
                    .WithIfComplete(() => _achievementModel.TotalSellNormalFishsushiAmount == 10)
                    .WithAward(() => _collectionModel.Gold.Value += 1));
            
            foreach (var (key, achievementInfo) in AchievementInfos)
            {
                if (!AchievementItems.ContainsKey(key))
                {
                    AchievementItems.Add(key, new AchievementItemInfo()
                        .WithKey(key)
                        .WithCompleteTime(null)
                        .WithCurrentAchievementState(AchievementState.Process));
                }
            }

            ActionKit.OnUpdate.Register(() =>
            {
                var key = IfHaveAchievementCanComplete();
                if (key != null)
                {
                    SettleAchievementAward(key);
                    AchievementInfos[key].Award();
                }
            });

        }

        public Dictionary<string, IAchievementInfo> AchievementInfos { get; } =
            new Dictionary<string, IAchievementInfo>();

        public Dictionary<string, IAchievementItemInfo> AchievementItems { get; } =
            new Dictionary<string, IAchievementItemInfo>();

        public string IfHaveAchievementCanComplete()
        {
            foreach (var (key, achievementItem) in AchievementItems.Where(item =>
                         item.Value.CurrentAchievementState.Value == AchievementState.Process))
            {
                if (AchievementInfos[key].IfComplete())
                {
                    return key;
                }
            }

            return null;
        }

        public void SettleAchievementAward(string key)
        {
            AchievementItems[key].CurrentAchievementState.Value = AchievementState.Finished;
        }

        private AchievementSystem AddAchievementSystem(string key, IAchievementInfo achievementInfo)
        {
            AchievementInfos.Add(key, achievementInfo);
            return this;
        }
    }
}