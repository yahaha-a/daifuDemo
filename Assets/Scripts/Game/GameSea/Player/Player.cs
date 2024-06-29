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
			
			mRigidbody2D = GetComponent<Rigidbody2D>();

			_playerFsm = new PlayerFsm(this);
			
			Events.HitFish.Register(fish =>
			{
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

			_playerModel.CurrentWeaponType.RegisterWithInitValue(type =>
			{
				if (type == WeaponTypes.FishFork)
				{
					FishFork.Show();
					Gun.Hide();
					MeleeWeapon.Hide();
				}
				else if (type == WeaponTypes.Gun)
				{
					FishFork.Hide();
					Gun.Show();
					MeleeWeapon.Hide();
				}
				else if (type == WeaponTypes.MeleeWeapon)
				{
					FishFork.Hide();
					Gun.Hide();
					MeleeWeapon.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			SwitchWeapons();
			
			this.SendCommand<PlayBreatheCommand>();
			
			_playerFsm.Tick();
		}

		private void SwitchWeapons()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				_playerModel.CurrentWeaponType.Value = WeaponTypes.FishFork;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_playerModel.CurrentWeaponType.Value = WeaponTypes.Gun;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				_playerModel.CurrentWeaponType.Value = WeaponTypes.MeleeWeapon;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
