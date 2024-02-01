using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class Events
    {
        public static EasyEvent<float, GameObject> WeaponAttackFish = new EasyEvent<float, GameObject>();

        public static EasyEvent<bool> FishForkIsNotUse = new EasyEvent<bool>();
        
        public static EasyEvent HitFish = new EasyEvent();

        public static EasyEvent CatchFish = new EasyEvent();

        public static EasyEvent FishForkHeadDestroy = new EasyEvent();

        public static EasyEvent PlayerIsHit = new EasyEvent();
    }
}