using System;
using System.Collections.Generic;
using Global;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class Gun : ViewController, IController, IWeapon
	{
		public string key { get; set; }
		
		public BindableProperty<int> currentRank { get; set; } = new BindableProperty<int>();
		
		public int MaxRank { get; set; }

		public string weaponName { get; set; }
		
		public Texture2D icon { get; set; }
		
		public BulletType bulletType;

		public float rateOfFire;

		public float attackRange;

		public int maximumAmmunition;

		public float intervalBetweenShots;

		public float loadAmmunitionNeedTime;

		public IndicatorType indicatorType;

		public List<(Vector2, float)> bulletSpawnLocationsAndDirectionsList;

		public float currentIntervalBetweenShots;

		public float currentLoadAmmunitionTime;

		public BindableProperty<int> currentAllAmmunition = new BindableProperty<int>(0);

		public BindableProperty<int> currentAmmunition = new BindableProperty<int>(0);

		public bool ifMeetReloadAmmunitionTime = false;
		
		public GameObject flyerRoot;
		
		private IGunModel _gunModel;

		private IPlayerModel _playerModel;

		private IWeaponSystem _weaponSystem;

		private GunFsm _gunFsm;

		private void Start()
		{
			_gunFsm = new GunFsm(this, BulletTemplate);
			
			_gunModel = this.GetModel<IGunModel>();
			
			_playerModel = this.GetModel<IPlayerModel>();

			flyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");

			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				_gunModel.IfLeft.Value = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			currentRank.Register(rank =>
			{
				InitData();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			_gunFsm.Tick();
		}

		public void InitData()
		{
			_weaponSystem = this.GetSystem<IWeaponSystem>();
			GunInfo currentGunInfo = (GunInfo)_weaponSystem.WeaponInfos[(key, currentRank.Value)];
			
			weaponName = currentGunInfo.Name;
			icon = currentGunInfo.Icon;
			attackRange = currentGunInfo.AttackRange;
			rateOfFire = currentGunInfo.RateOfFire;
			maximumAmmunition = currentGunInfo.MaximumAmmunition;
			loadAmmunitionNeedTime = currentGunInfo.LoadAmmunitionNeedTime;
			intervalBetweenShots = currentGunInfo.IntervalBetweenShots;
			indicatorType = currentGunInfo.IndicatorType;
			bulletSpawnLocationsAndDirectionsList = currentGunInfo.BulletSpawnLocationsAndDirectionsList;
			MaxRank = currentGunInfo.MaxRank;

			currentIntervalBetweenShots = intervalBetweenShots;
			currentLoadAmmunitionTime = loadAmmunitionNeedTime;

			if (currentAllAmmunition.Value - maximumAmmunition >= 0)
			{
				currentAmmunition.Value = maximumAmmunition;
				currentAllAmmunition.Value -= maximumAmmunition;
			}
			else
			{
				currentAmmunition.Value = currentAllAmmunition.Value;
				currentAllAmmunition.Value = 0;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
