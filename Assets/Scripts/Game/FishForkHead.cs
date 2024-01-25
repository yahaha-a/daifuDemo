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

		private Rigidbody2D _rigidbody2D;

		private FishForkHeadState _fishForkHeadState = FishForkHeadState.FLY;

		public Transform originTransform;
		
		public static EasyEvent HitFish = new EasyEvent();

		public static EasyEvent CatchFish = new EasyEvent();

		private Vector3 direction;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Fish"))
			{
				transform.parent = other.transform;
				_fishForkHeadState = FishForkHeadState.HIT;
				HitFish?.Trigger();
			}
			else
			{
				gameObject.DestroySelf();
			}
		}

		private void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			
			direction = originTransform.right.normalized * FindObjectOfType<Player>().transform.localScale.x;
		}

		private void Update()
		{
			if (_fishForkHeadState == FishForkHeadState.FLY)
			{
				var targetSpeed = direction * _speed;
				_rigidbody2D.velocity =
					Vector2.Lerp(_rigidbody2D.velocity, targetSpeed, 1 - Mathf.Exp(-Time.deltaTime * 10));

				if (Vector3.Distance(transform.position, originTransform.position) > _fishForkLength)
				{
					gameObject.DestroySelf();
				}
			}
		}
	}
}
