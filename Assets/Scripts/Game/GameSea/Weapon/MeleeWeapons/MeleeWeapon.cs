using System;
using System.Collections;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class MeleeWeapon : ViewController, IController
	{
		public bool IfLeft { get; private set; } = false;

		public float Damage { get; private set; }

		public float AttackRadius { get; private set; }

		public float AttackFrequency { get; private set; }

		private Vector3 _menchoDirection;

		private IPlayerModel _playerModel;

		private IMeleeWeaponModel _meleeWeaponModel;

		private bool _isAttack;

		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();

			_meleeWeaponModel = this.GetModel<IMeleeWeaponModel>();

			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				IfLeft = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_meleeWeaponModel.CurrentMeleeWeaponKey.RegisterWithInitValue(key =>
			{
				UpdateData();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_meleeWeaponModel.CurrentMeleeWeaponRank.RegisterWithInitValue(rank =>
			{
				UpdateData();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.Attack.Register(() =>
			{
				var fishes = GameObject.FindGameObjectsWithTag("Fish");

				foreach (var fish in fishes)
				{
					var distance = Vector2.Distance(fish.transform.position, transform.position);
					if (distance <= AttackRadius)
					{
						var direction = fish.transform.position - transform.position;
						var angle = Vector2.Angle(direction, _menchoDirection);
						if (angle <= 60f)
						{
							this.SendCommand(new WeaponAttackFishCommand(Damage, fish));
						}
					}
				}

				ActionKit.Delay(AttackFrequency, () =>
				{
					_playerModel.IfAttacking.Value = false;
				}).Start(this);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.Attack2.Register(() =>
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

		private void UpdateData()
		{
			Damage = this.SendQuery(new FindMeleeWeaponDamage(_meleeWeaponModel.CurrentMeleeWeaponKey.Value,
				_meleeWeaponModel.CurrentMeleeWeaponRank.Value));
			AttackRadius = this.SendQuery(new FindMeleeWeaponAttackRadius(_meleeWeaponModel.CurrentMeleeWeaponKey.Value,
				_meleeWeaponModel.CurrentMeleeWeaponRank.Value));
			AttackFrequency = this.SendQuery(new FindMeleeWeaponAttackFrequency(
				_meleeWeaponModel.CurrentMeleeWeaponKey.Value, _meleeWeaponModel.CurrentMeleeWeaponRank.Value));
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
