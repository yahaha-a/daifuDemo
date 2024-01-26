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
	
	public partial class Player : ViewController, IController
	{
		private Rigidbody2D _mRigidbody2D;

		private PlayState _playState = PlayState.SWIM;

		
		private void Awake()
		{
			_mRigidbody2D = GetComponent<Rigidbody2D>();

			FishForkHead.HitFish.Register(() =>
			{
				_playState = PlayState.CATCH_FISH;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			FishForkHead.CatchFish.Register(() =>
			{
				_playState = PlayState.SWIM;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (FishFork._fishForkState == FishForkState.AIM || FishFork._fishForkState == FishForkState.REVOLVE)
			{
				_playState = PlayState.AIM;
			}
			else
			{
				_playState = PlayState.SWIM;
			}
			
			if (_playState == PlayState.SWIM)
			{
				var inputHorizontal = Input.GetAxis("Horizontal");
				var inputVertical = Input.GetAxis("Vertical");

				if (inputHorizontal < 0)
				{
					transform.localScale = new Vector3(-1, 1, 0);
					FishFork._ifLeft = true;
				}
				else if (inputHorizontal > 0)
				{
					transform.localScale = new Vector3(1, 1, 0);
					FishFork._ifLeft = false;
				}
				
				var direction = new Vector2(inputHorizontal, inputVertical).normalized;
				var playerTargetWalkingSpeed = direction * Config.PlayerWalkingRate;
				_mRigidbody2D.velocity = Vector2.Lerp(_mRigidbody2D.velocity, playerTargetWalkingSpeed,
					1 - Mathf.Exp(-Time.deltaTime * 10));
			}
			else if (_playState == PlayState.AIM)
			{
				_mRigidbody2D.velocity = Vector2.zero;
			}
			else if (_playState == PlayState.CATCH_FISH)
			{
				_mRigidbody2D.velocity = Vector2.zero;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
