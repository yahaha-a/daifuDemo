using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IWeapon
    {
        public string key { get; set; }

        public BindableProperty<int> currentRank { get; set; }
        
        public int MaxRank { get; set; }

        public string weaponName { get; set; }

        public Texture2D icon { get; set; }
    }
}