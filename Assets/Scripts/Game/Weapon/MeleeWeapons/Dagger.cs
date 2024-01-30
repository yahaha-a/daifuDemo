using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class Dagger : ViewController, IMeleeWeapons, IController
	{
		public bool IfLeft { get; private set; } = false;

		public float Damage { get; private set; } = 5f;

		public float AttackRadius { get; private set; } = 10f;

		public float AttackFrequency { get; private set; } = 0.5f;

		private Vector3 _menchoDirection;

		private void Start()
		{
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
				var fishes = GameObject.FindGameObjectsWithTag("Fish");
				
				foreach (var fish in fishes)
				{
					var distance = Vector2.Distance(fish.transform.position, transform.position);
					if (distance <= AttackRadius)
					{
						var direction = fish.transform.position - transform.position;
						var angle = Vector2.Angle(direction, _menchoDirection);
						Debug.Log(angle);
						if (angle <= 60f)
						{
							Events.WeaponAttackFish?.Trigger(Damage, fish);
						}
					}
				}
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
