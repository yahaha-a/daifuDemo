using System;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public enum FishForkHeadState
	{
		Fly,
		Hit
	}
	
	public partial class FishForkHead : ViewController, IController
	{
		public float speed;

		public float fishForkLength;
		
		private Rigidbody2D _rigidbody2D;

		private FishForkHeadState _fishForkHeadState = FishForkHeadState.Fly;

		private Vector3 _originPosition;

		private IFishForkModel _fishForkModel;

		private IFishForkHeadModel _fishForkHeadModel;

		private IWeaponSystem _weaponSystem;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (_fishForkHeadState == FishForkHeadState.Fly && other.CompareTag("FishHitBox"))
			{
				transform.parent = other.transform;
				_fishForkHeadState = FishForkHeadState.Hit;
				Events.HitFish?.Trigger(other.transform.parent.gameObject);
			}
		}

		private void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();

			_originPosition = transform.position;

			_fishForkModel = this.GetModel<IFishForkModel>();

			_fishForkHeadModel = this.GetModel<IFishForkHeadModel>();

			_weaponSystem = this.GetSystem<IWeaponSystem>();
			
			Events.CatchFish.Register(fish =>
			{
				Events.FishForkHeadDestroy?.Trigger();
				gameObject.DestroySelf();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Events.FishEscape.Register(fish =>
			{
				Events.FishForkHeadDestroy?.Trigger();
				gameObject.DestroySelf();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (_fishForkHeadState == FishForkHeadState.Fly)
			{
				var speed = transform.right.normalized * this.speed * _fishForkHeadModel.FishForkHeadDirection;
				var position = transform.position;
				transform.position = new Vector3(position.x + speed.x * Time.deltaTime,
					position.y + speed.y * Time.deltaTime, position.z);
				
				if (Vector3.Distance(transform.position, _originPosition) > fishForkLength)
				{
					Events.FishForkHeadDestroy?.Trigger();
					gameObject.DestroySelf();
				}
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
