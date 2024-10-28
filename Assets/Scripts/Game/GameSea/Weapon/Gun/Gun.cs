using System.Collections.Generic;
using Global;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class Gun : ViewController, IController
	{
		public GameObject flyerRoot;
		
		private IGunModel _gunModel;

		private IPlayerModel _playerModel;

		private ISingleUseItemsModel _singleUseItemsModel;
		
		private IWeaponSystem _weaponSystem;

		private GunFsm _gunFsm;

		private void Start()
		{
			_gunFsm = new GunFsm(this, BulletTemplate);
			
			_gunModel = this.GetModel<IGunModel>();
			
			_playerModel = this.GetModel<IPlayerModel>();

			_singleUseItemsModel = this.GetModel<ISingleUseItemsModel>();
			
			_weaponSystem = this.GetSystem<IWeaponSystem>();
			
			flyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");
			

			_gunModel.CurrentGunKey.RegisterWithInitValue(key =>
			{
				UpdateData();
			});
			
			_gunModel.CurrentRank.RegisterWithInitValue(rank =>
			{
				UpdateData();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			

			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				_gunModel.IfLeft.Value = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			_gunFsm.Tick();
		}

		public void UpdateData()
		{
			GunInfo currentGunInfo = (GunInfo)_weaponSystem.WeaponInfos[(_gunModel.CurrentGunKey.Value, _gunModel.CurrentRank.Value)];
			_gunModel.GunName.Value = currentGunInfo.Name;
			_gunModel.Icon.Value = currentGunInfo.Icon;
			_gunModel.AttackRange.Value = currentGunInfo.AttackRange;
			_gunModel.RateOfFire.Value = currentGunInfo.RateOfFire;
			_gunModel.MaximumAmmunition.Value = currentGunInfo.MaximumAmmunition;
			_gunModel.LoadAmmunitionNeedTime.Value = currentGunInfo.LoadAmmunitionNeedTime;
			_gunModel.IntervalBetweenShots.Value = currentGunInfo.IntervalBetweenShots;
			_gunModel.BulletSpawnLocationsAndDirectionsList.Value = currentGunInfo.BulletSpawnLocationsAndDirectionsList;

			_gunModel.CurrentIntervalBetweenShots.Value = _gunModel.IntervalBetweenShots.Value;
			_gunModel.CurrentLoadAmmunitionTime.Value = _gunModel.LoadAmmunitionNeedTime.Value;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
