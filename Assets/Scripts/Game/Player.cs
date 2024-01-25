using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum PlayState
	{
		SWIM,
		AIM,
		CATCH_FISH
	}
	
	public partial class Player : ViewController
	{
		private Rigidbody2D _mRigidbody2D;

		private PlayState _state = PlayState.SWIM;

		private void Awake()
		{
			_mRigidbody2D = GetComponent<Rigidbody2D>();

			FishForkHead.HitFish.Register(() =>
			{
				_state = PlayState.CATCH_FISH;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			FishForkHead.CatchFish.Register(() =>
			{
				_state = PlayState.SWIM;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (FishFork._fishForkState == FishForkState.AIM || FishFork._fishForkState == FishForkState.REVOLVE)
			{
				_state = PlayState.AIM;
			}
			else
			{
				_state = PlayState.SWIM;
			}
			
			if (_state == PlayState.SWIM)
			{
				var inputHorizontal = Input.GetAxis("Horizontal");
				var inputVertical = Input.GetAxis("Vertical");

				if (inputHorizontal < 0)
				{
					transform.localScale = new Vector3(-1, 1, 0);
				}
				else if (inputHorizontal > 0)
				{
					transform.localScale = new Vector3(1, 1, 0);
				}
				
				var direction = new Vector2(inputHorizontal, inputVertical).normalized;
				var playerTargetWalkingSpeed = direction * Config.PlayerWalkingRate;
				_mRigidbody2D.velocity = Vector2.Lerp(_mRigidbody2D.velocity, playerTargetWalkingSpeed,
					1 - Mathf.Exp(-Time.deltaTime * 10));
			}
			else if (_state == PlayState.AIM)
			{
				_mRigidbody2D.velocity = Vector2.zero;
			}
			else if (_state == PlayState.CATCH_FISH)
			{
				_mRigidbody2D.velocity = Vector2.zero;
			}
		}
	}
}
