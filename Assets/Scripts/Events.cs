using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class Events
    {
        public static EasyEvent<bool> PlayerVeer = new EasyEvent<bool>();

        public static EasyEvent<float, GameObject> WeaponAttackFish = new EasyEvent<float, GameObject>();

        public static EasyEvent<bool> FishForkIsNotUse = new EasyEvent<bool>();
    }
}