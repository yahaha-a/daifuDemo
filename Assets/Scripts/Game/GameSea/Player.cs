using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class Player : ViewController, IController
	{
		private Rigidbody2D _mRigidbody2D;

		private IPlayerModel _playerModel;

		private IFishForkModel _fishForkModel;

		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();

			_fishForkModel = this.GetModel<IFishForkModel>();
			
			_mRigidbody2D = GetComponent<Rigidbody2D>();

			Events.HitFish.Register(fish =>
			{
				_playerModel.State.Value = PlayState.CatchFish;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.CatchFish.Register(fish =>
			{
				_playerModel.State.Value = PlayState.Swim;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_fishForkModel.FishForkIfShooting.Register(value =>
			{
				if (value)
				{
					_playerModel.State.Value = PlayState.Aim;
				}
				else
				{
					_playerModel.State.Value = PlayState.Swim;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_fishForkModel.CurrentFishForkState.Register(value =>
			{
				if (value == FishForkState.Aim)
				{
					_playerModel.State.Value = PlayState.Aim;
				}
				else if (value == FishForkState.Ready)
				{
					_playerModel.State.Value = PlayState.Swim;
				}
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

			_playerModel.IfChestOpening.Register(value =>
			{
				if (!value)
				{
					_playerModel.OpenChestSeconds.Value = 0;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			SwitchWeapons();
			
			this.SendCommand<PlayBreatheCommand>();
			
			if (_playerModel.State.Value == PlayState.Swim)
			{
				var inputHorizontal = Input.GetAxis("Horizontal");
				var inputVertical = Input.GetAxis("Vertical");

				if (inputHorizontal < 0)
				{
					transform.localScale = new Vector3(-1, 1, 0);
					
					_playerModel.IfLeft.Value = true;
				}
				else if (inputHorizontal > 0)
				{
					transform.localScale = new Vector3(1, 1, 0);
					
					_playerModel.IfLeft.Value = false;
				}
				
				var direction = new Vector2(inputHorizontal, inputVertical).normalized;
				var playerTargetWalkingSpeed = direction * Config.PlayerWalkingRate;
				_mRigidbody2D.velocity = Vector2.Lerp(_mRigidbody2D.velocity, playerTargetWalkingSpeed,
					1 - Mathf.Exp(-Time.deltaTime * 10));
			}
			else if (_playerModel.State.Value == PlayState.Aim)
			{
				_mRigidbody2D.velocity = Vector2.zero;
			}
			else if (_playerModel.State.Value == PlayState.CatchFish)
			{
				_mRigidbody2D.velocity = Vector2.zero;
			}
			else if (_playerModel.State.Value == PlayState.OpenTreasureChests)
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					_playerModel.IfChestOpening.Value = true;
					_mRigidbody2D.velocity = Vector2.zero;
				}
				if (Input.GetKeyUp(KeyCode.E))
				{
					_playerModel.IfChestOpening.Value = false;
				}

				if (_playerModel.IfChestOpening.Value)
				{
					_playerModel.OpenChestSeconds.Value += Time.deltaTime;
				}
			}
			else if (_playerModel.State.Value == PlayState.PickUp)
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					_playerModel.State.Value = PlayState.PickUpEd;
				}
			}
			
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
