using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class Events
    {
        public static EasyEvent<float, GameObject> WeaponAttackFish = new EasyEvent<float, GameObject>();

        public static EasyEvent<bool> FishForkIsNotUse = new EasyEvent<bool>();
        
        public static EasyEvent<GameObject> HitFish = new EasyEvent<GameObject>();

        public static EasyEvent<IFish> CatchFish = new EasyEvent<IFish>();

        public static EasyEvent FishForkHeadDestroy = new EasyEvent();

        public static EasyEvent PlayerIsHit = new EasyEvent();

        public static EasyEvent GamePass = new EasyEvent();

        public static EasyEvent UISettlePanelShow = new EasyEvent();

        public static EasyEvent<BackPackItemTemplate> UIShipBackPackPanelItemInfoUpdate = new EasyEvent<BackPackItemTemplate>();

        public static EasyEvent<TreasureBox> TreasureBoxOpened = new EasyEvent<TreasureBox>();

        public static EasyEvent<PickUpItem> ItemPickUped = new EasyEvent<PickUpItem>();

        public static EasyEvent<string> UIGamesushiIngredientPanelUpdate = new EasyEvent<string>();

        public static EasyEvent UpgradeMenu = new EasyEvent();

        public static EasyEvent CommencedBusiness = new EasyEvent();

        public static EasyEvent FinishBusiness = new EasyEvent();

        public static EasyEvent<Vector2, string> CreateCustomerOrderMenuIcon = new EasyEvent<Vector2, string>();

        public static EasyEvent TakeFirstFinishedDish = new EasyEvent();

        public static EasyEvent<IPreparationDishInfo, float> CookerMakingDishesQueueAdd =
            new EasyEvent<IPreparationDishInfo, float>();

        public static EasyEvent ReceptionACustomer = new EasyEvent();
    }
}