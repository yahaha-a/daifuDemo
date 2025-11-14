using System;
using System.Collections;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class MeleeWeapon : ViewController, IController, IWeapon
	{
		public string key { get; set; }

		public BindableProperty<int> currentRank { get; set; } = new BindableProperty<int>();
		
		public int MaxRank { get; set; }

		public string weaponName { get; set; }
		
		public Texture2D icon { get; set; }
		
		public bool IfLeft { get; private set; } = false;

		public float Damage { get; private set; }

		public float AttackRadius { get; private set; }

		public float AttackFrequency { get; private set; }

		private Vector3 _menchoDirection;

		private IPlayerModel _playerModel;

		private IMeleeWeaponModel _meleeWeaponModel;

		private IWeaponSystem _weaponSystem;

		private bool _isAttack;

		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();

			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				IfLeft = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.Attack.Register(() =>
			{
				if (!_isAttack)
				{
					StartCoroutine(Attack());
					
					ActionKit.Delay(AttackFrequency, () =>
					{
						_playerModel.IfAttacking.Value = false;
					}).Start(this);
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			currentRank.Register(rank =>
			{
				InitData();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (IfLeft)
			{
				_menchoDirection = -transform.right;
			}
			else
			{
				_menchoDirection = transform.right;
			}
		}

		public void InitData()
		{
			_weaponSystem = this.GetSystem<IWeaponSystem>();
			_meleeWeaponModel = this.GetModel<IMeleeWeaponModel>();
			MeleeWeaponInfo currentMeleeWeaponInfo = (MeleeWeaponInfo)_weaponSystem.WeaponInfos[
				(_meleeWeaponModel.CurrentMeleeWeaponKey.Value, _meleeWeaponModel.CurrentMeleeWeaponRank.Value)];

			Damage = currentMeleeWeaponInfo.Damage;
			AttackRadius = currentMeleeWeaponInfo.AttackRadius;
			AttackFrequency = currentMeleeWeaponInfo.AttackFrequency;
			MaxRank = currentMeleeWeaponInfo.MaxRank;
		}

		IEnumerator Attack()
		{
			Icon.Show();
			HitBox.Show();
			
			_isAttack = true;

			float rotationAmount;
			
			if (IfLeft)
			{
				rotationAmount = 80f;
			}
			else
			{
				rotationAmount = -80f;
			}
			
			float rotationDuration = 0.3f;
			
			float currentRotationTime = 0f;

			Quaternion initialRotation = transform.rotation;
			
			Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, rotationAmount);
			
			while (currentRotationTime < rotationDuration)
			{
				currentRotationTime += Time.deltaTime;
				float t = Mathf.Clamp01(currentRotationTime / rotationDuration);
				transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
				yield return null;
			}

			transform.rotation = initialRotation;
			_isAttack = false;
			
			Icon.Hide();
			HitBox.Hide();
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
