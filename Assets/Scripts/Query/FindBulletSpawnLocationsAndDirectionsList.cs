using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class FindBulletSpawnLocationsAndDirectionsList : AbstractQuery<List<(Vector2, float)>>
    {
        private string _gunKey;

        private int _rank;
        
        public FindBulletSpawnLocationsAndDirectionsList(string gunKey, int rank)
        {
            _gunKey = gunKey;
            _rank = rank;
        }
        
        protected override List<(Vector2, float)> OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var bulletSpawnLocationsAndDirectionsList =
                weaponSystem.GunInfos[(_gunKey, _rank)].BulletSpawnLocationsAndDirectionsList;
            return bulletSpawnLocationsAndDirectionsList;
        }
    }
}