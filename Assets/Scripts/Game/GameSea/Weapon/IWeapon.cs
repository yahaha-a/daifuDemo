using UnityEngine;

namespace daifuDemo
{
    public interface IWeapon
    {
        public string key { get; }

        public int currentRank { get; }
        
        public string weaponName { get; }

        public Texture2D icon { get; }
    }
}