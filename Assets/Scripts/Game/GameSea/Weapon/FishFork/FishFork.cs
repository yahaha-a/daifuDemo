using System;
using UnityEngine;
using QFramework;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class FishFork : ViewController, IController, IWeapon
	{
		public string key { get; set; }
		
		public int currentRank { get; set; }
		
		public string weaponName { get; set; }
		
		public Texture2D icon { get; set; }

		public float launchSpeed;

		public float fishForkLength;
		
		public float chargingTime;

		public float currentChargingTime;

		public float currentFishForkLength;
		
		public bool ifLeft;

		private IPlayerModel _playerModel;

		private IFishForkModel _fishForkModel;

		private IWeaponSystem _weaponSystem;

		private FishForkFsm _fishForkFsm;

		private void Start()
		{
			_fishForkFsm = new FishForkFsm(this);
			
			_playerModel = this.GetModel<IPlayerModel>();

			_fishForkModel = this.GetModel<IFishForkModel>();

			_weaponSystem = this.GetSystem<IWeaponSystem>();

			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				ifLeft = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FishForkHeadDestroy.Register(() =>
			{
				_fishForkModel.IfFishForkHeadExist.Value = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			InitData();
		}

		private void Update()
		{
			_fishForkFsm.Tick();
		}

		private void InitData()
		{
			FishForkInfo fishForkInfo = (FishForkInfo)_weaponSystem.WeaponInfos[(key, currentRank)];
			launchSpeed = fishForkInfo.LaunchSpeed;
			fishForkLength = fishForkInfo.FishForkLength;
			chargingTime = fishForkInfo.ChargingTime;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
