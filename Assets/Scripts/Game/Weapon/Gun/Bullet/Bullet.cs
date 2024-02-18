using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class Bullet : ViewController, IController
	{
		private float _damage;

		private float _speed;

		private float _range;
		
		private Vector3 _originPosition;

		public int Direction;

		private Rigidbody2D _rigidbody2D;

		private IGunModel _gunModel;

		private IBulletModel _bulletModel;

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

			_gunModel = this.GetModel<IGunModel>();

			_bulletModel = this.GetModel<IBulletModel>();

			_gunModel.CurrentGunKey.RegisterWithInitValue(key =>
			{
				_bulletModel.CurrentBulletAttribute.Value = BulletAttribute.Normal;
				_bulletModel.CurrentBulletRank.Value = 1;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_bulletModel.CurrentBulletRank.RegisterWithInitValue(rank =>
			{
				_damage = this.SendQuery(new FindBulletDamage(_gunModel.CurrentGunKey.Value,
					_bulletModel.CurrentBulletAttribute.Value, _bulletModel.CurrentBulletRank.Value));
				_speed = this.SendQuery(new FindBulletSpeed(_gunModel.CurrentGunKey.Value,
					_bulletModel.CurrentBulletAttribute.Value, _bulletModel.CurrentBulletRank.Value));
				_range = this.SendQuery(new FindBulletRange(_gunModel.CurrentGunKey.Value,
					_bulletModel.CurrentBulletAttribute.Value, _bulletModel.CurrentBulletRank.Value));
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_bulletModel.CurrentBulletAttribute.RegisterWithInitValue(bulletAttribute =>
			{
				_damage = this.SendQuery(new FindBulletDamage(_gunModel.CurrentGunKey.Value,
					_bulletModel.CurrentBulletAttribute.Value, _bulletModel.CurrentBulletRank.Value));
				_speed = this.SendQuery(new FindBulletSpeed(_gunModel.CurrentGunKey.Value,
					_bulletModel.CurrentBulletAttribute.Value, _bulletModel.CurrentBulletRank.Value));
				_range = this.SendQuery(new FindBulletRange(_gunModel.CurrentGunKey.Value,
					_bulletModel.CurrentBulletAttribute.Value, _bulletModel.CurrentBulletRank.Value));
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			var speed = transform.right.normalized * _speed * Direction;
			var position = transform.position;
			transform.position = new Vector3(position.x + speed.x * Time.deltaTime,
				position.y + speed.y * Time.deltaTime, position.z);
				
			if (Vector3.Distance(transform.position, _originPosition) > _range)
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
