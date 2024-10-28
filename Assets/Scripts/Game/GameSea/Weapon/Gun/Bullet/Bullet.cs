using System;
using Global;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class Bullet : ViewController, IController
	{
		private Vector3 _originPosition;
		
		public float damage;
		
		public float speed;

		public float range;

		public int direction;

		public BulletType bulletType;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("FishHitBox"))
			{
				if (bulletType == BulletType.Normal)
				{
					this.SendCommand(new WeaponAttackFishCommand(damage, other.transform.parent.gameObject));
					gameObject.DestroySelf();
				}
				else if (bulletType == BulletType.Hypnosis)
				{
					
				}
			}
		}

		private void Start()
		{
			_originPosition = transform.position;
		}

		private void Update()
		{
			var currentSpeed = transform.right.normalized * speed * direction;
			var position = transform.position;
			transform.position = new Vector3(position.x + currentSpeed.x * Time.deltaTime,
				position.y + currentSpeed.y * Time.deltaTime, position.z);
				
			if (Vector3.Distance(transform.position, _originPosition) > range)
			{
				gameObject.DestroySelf();
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
