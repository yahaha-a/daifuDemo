using System;
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
			
			if (Input.GetKeyDown(KeyCode.J))
			{
				if (_playerModel.State.Value == PlayState.Swim)
				{
					_playerModel.State.Value = PlayState.Attack;
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
						_playerModel.State.Value = PlayState.Swim;
					}).Start(this);
				}
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

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}

	}
}
