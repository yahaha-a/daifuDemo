using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum PlayState
	{
		Swim,
		Aim,
		CatchFish
	}
	
	public partial class Player : ViewController, IController
	{
		private Rigidbody2D _mRigidbody2D;

		private PlayState _playState = PlayState.Swim;

		
		private void Awake()
		{
			_mRigidbody2D = GetComponent<Rigidbody2D>();

			FishForkHead.HitFish.Register(() =>
			{
				_playState = PlayState.CatchFish;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			FishForkHead.CatchFish.Register(() =>
			{
				_playState = PlayState.Swim;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (FishFork._fishForkState == FishForkState.Aim || FishFork._fishForkState == FishForkState.Revolve)
			{
				_playState = PlayState.Aim;
			}
			else
			{
				_playState = PlayState.Swim;
			}
			
			if (_playState == PlayState.Swim)
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
			else if (_playState == PlayState.Aim)
			{
				_mRigidbody2D.velocity = Vector2.zero;
			}
			else if (_playState == PlayState.CatchFish)
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
