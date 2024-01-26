using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum FishForkHeadState
	{
		FLY,
		HIT
	}
	
	public partial class FishForkHead : ViewController
	{
		private float _speed = 10f;

		private float _fishForkLength = 20f;

		public int Direction = 1;

		private Rigidbody2D _rigidbody2D;

		private FishForkHeadState _fishForkHeadState = FishForkHeadState.FLY;

		private Vector3 _originPosition;
		
		public static EasyEvent HitFish = new EasyEvent();

		public static EasyEvent CatchFish = new EasyEvent();

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Fish"))
			{
				transform.parent = other.transform;
				_fishForkHeadState = FishForkHeadState.HIT;
				HitFish?.Trigger();
			}
		}

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();

			_originPosition = transform.position;
		}

		private void Update()
		{
			if (_fishForkHeadState == FishForkHeadState.FLY)
			{
				var speed = transform.right.normalized * _speed * Direction;
				var position = transform.position;
				transform.position = new Vector3(position.x + speed.x * Time.deltaTime,
					position.y + speed.y * Time.deltaTime, position.z);
				
				if (Vector3.Distance(transform.position, _originPosition) > _fishForkLength)
				{
					gameObject.DestroySelf();
				}
			}
		}
	}
}
