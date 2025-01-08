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
		
		public int currentRank { get; set; }
		
		public string weaponName { get; set; }
		
		public Texture2D icon { get; set; }
		
		public BulletType bulletType;

		public float rateOfFire;

		public float attackRange;

		public int maximumAmmunition;

		public float intervalBetweenShots;

		public float loadAmmunitionNeedTime;

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

			_weaponSystem = this.GetSystem<IWeaponSystem>();
			
			flyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");

			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				_gunModel.IfLeft.Value = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			InitData();
		}

		private void Update()
		{
			_gunFsm.Tick();
		}

		public void InitData()
		{
			GunInfo currentGunInfo = (GunInfo)_weaponSystem.WeaponInfos[(key, currentRank)];
			weaponName = currentGunInfo.Name;
			icon = currentGunInfo.Icon;
			attackRange = currentGunInfo.AttackRange;
			rateOfFire = currentGunInfo.RateOfFire;
			maximumAmmunition = currentGunInfo.MaximumAmmunition;
			loadAmmunitionNeedTime = currentGunInfo.LoadAmmunitionNeedTime;
			intervalBetweenShots = currentGunInfo.IntervalBetweenShots;
			bulletSpawnLocationsAndDirectionsList = currentGunInfo.BulletSpawnLocationsAndDirectionsList;

			currentIntervalBetweenShots = intervalBetweenShots;
			currentLoadAmmunitionTime = loadAmmunitionNeedTime;
			currentAmmunition = currentAllAmmunition;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
