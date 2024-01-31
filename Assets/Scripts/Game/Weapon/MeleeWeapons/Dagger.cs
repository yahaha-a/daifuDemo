using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class Dagger : ViewController, IMeleeWeapons, IController
	{
		public string Key { get; } = Config.DaggerKey;
		
		public bool IfLeft { get; private set; } = false;

		public float Damage { get; private set; } = 5f;

		public float AttackRadius { get; private set; } = 10f;

		public float AttackFrequency { get; private set; } = 0.5f;

		private Vector3 _menchoDirection;

		private IPlayerModel _playerModel;

		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();
			
			Events.PlayerVeer.Register(value =>
			{
				IfLeft = value;
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
					_playerModel.State.Value = PlayState.Swim;
				}
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}

	}
}
