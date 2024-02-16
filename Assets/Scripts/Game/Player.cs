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

		public List<GameObject> WeaponList;

		BindableProperty<GameObject> CurrentWeapon = new BindableProperty<GameObject>();
		
		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();
			
			_mRigidbody2D = GetComponent<Rigidbody2D>();

			Events.HitFish.Register(fish =>
			{
				_playerModel.State.Value = PlayState.CatchFish;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.CatchFish.Register(fish =>
			{
				_playerModel.State.Value = PlayState.Swim;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FishForkIsNotUse.Register(value =>
			{
				if (value)
				{
					_playerModel.State.Value = PlayState.Swim;
				}
				else
				{
					_playerModel.State.Value = PlayState.Aim;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_playerModel.WeaponKey.RegisterWithInitValue(value =>
			{
				foreach (var weapon in WeaponList)
				{
					if (value == weapon.GetComponent<Iweapon>().Key)
					{
						CurrentWeapon.Value = weapon;
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			CurrentWeapon.RegisterWithInitValue(value =>
			{
				foreach (Transform child in transform)
				{
					child.gameObject.DestroySelf();
				}

				value.InstantiateWithParent(this);
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
		}

		private void SwitchWeapons()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.GetModel<IPlayerModel>().WeaponKey.Value = Config.FishForkKey;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.GetModel<IPlayerModel>().WeaponKey.Value = Config.DaggerKey;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.GetModel<IPlayerModel>().WeaponKey.Value = Config.RifleKey;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
