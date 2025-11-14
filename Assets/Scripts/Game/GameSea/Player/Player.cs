using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class Player : ViewController, IController
	{
		public Rigidbody2D mRigidbody2D;

		private IPlayerModel _playerModel;

		private IFishForkModel _fishForkModel;

		private IWeaponSystem _weaponSystem;

		private PlayerFsm _playerFsm;
		
		public int GetFishChallengeClicks => _playerModel.FishingChallengeClicks.Value;

		public void ResetFishChallengeClicks()
		{
			_playerModel.FishingChallengeClicks.Value = 0;
		}

		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();

			_fishForkModel = this.GetModel<IFishForkModel>();

			_weaponSystem = this.GetSystem<IWeaponSystem>();
			
			mRigidbody2D = GetComponent<Rigidbody2D>();

			_playerFsm = new PlayerFsm(this);
			
			Events.HitFish.Register(fish =>
			{
				_playerModel.MaxFishingChallengeClicks.Value = fish.GetComponent<IFish>().Clicks;
				_playerModel.IfCatchingFish.Value = true;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.CatchFish.Register(fish =>
			{
				_playerModel.IfCatchingFish.Value = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FishEscape.Register(fish =>
			{
				_playerModel.IfCatchingFish.Value = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_playerModel.CurrentWeaponType.RegisterWithInitValue(key =>
			{
				_weaponSystem.SwitchWeapons(key);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			SwitchWeapons();
			
			this.SendCommand<PlayBreatheCommand>();
			
			_playerFsm.Tick();

			if (_playerModel.CurrentState.Value == PlayState.Swim)
			{
				_playerModel.CurrentPosition.Value = transform.position;
			}
		}

		private void SwitchWeapons()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				_playerModel.CurrentWeaponType.Value = EquipWeaponKey.FishFork;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_playerModel.CurrentWeaponType.Value = EquipWeaponKey.MeleeWeapon;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				_playerModel.CurrentWeaponType.Value = EquipWeaponKey.PrimaryWeapon;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				_playerModel.CurrentWeaponType.Value = EquipWeaponKey.SecondaryWeapons;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
