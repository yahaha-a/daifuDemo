using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class Bullet : ViewController, IController
	{
		private float _damage = 5f;

		private float _speed = 20f;

		private float range = 20f;
		
		private Vector3 _originPosition;

		public int Direction = 1;

		private Rigidbody2D _rigidbody2D;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("FishHitBox"))
			{
				this.SendCommand(new WeaponAttackFishCommand(_damage, other.transform.parent.gameObject));
				gameObject.DestroySelf();
			}
		}

		private void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			
			_originPosition = transform.position;
		}

		private void Update()
		{
			var speed = transform.right.normalized * _speed * Direction;
			var position = transform.position;
			transform.position = new Vector3(position.x + speed.x * Time.deltaTime,
				position.y + speed.y * Time.deltaTime, position.z);
				
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
