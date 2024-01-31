using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum FishForkHeadState
	{
		Fly,
		Hit
	}
	
	public partial class FishForkHead : ViewController
	{
		private float _speed = 30f;

		private float _fishForkLength = 10f;

		public int Direction = 1;

		private Rigidbody2D _rigidbody2D;

		private FishForkHeadState _fishForkHeadState = FishForkHeadState.Fly;

		private Vector3 _originPosition;
		
		public static EasyEvent HitFish = new EasyEvent();

		public static EasyEvent CatchFish = new EasyEvent();

		public static EasyEvent FishForkHeadDestroy = new EasyEvent();

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("FishHitBox"))
			{
				transform.parent = other.transform;
				_fishForkHeadState = FishForkHeadState.Hit;
				HitFish?.Trigger();
			}
		}

		private void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();

			_originPosition = transform.position;
		}

		private void Update()
		{
			if (_fishForkHeadState == FishForkHeadState.Fly)
			{
				var speed = transform.right.normalized * _speed * Direction;
				var position = transform.position;
				transform.position = new Vector3(position.x + speed.x * Time.deltaTime,
					position.y + speed.y * Time.deltaTime, position.z);
				
				if (Vector3.Distance(transform.position, _originPosition) > _fishForkLength)
				{
					FishForkHeadDestroy?.Trigger();
					gameObject.DestroySelf();
				}
			}
		}
	}
}
