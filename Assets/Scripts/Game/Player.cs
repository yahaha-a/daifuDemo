using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class Player : ViewController
	{
		private Rigidbody2D _mRigidbody2D;

		private void Awake()
		{
			_mRigidbody2D = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			var inputHorizontal = Input.GetAxis("Horizontal");
			var inputVertical = Input.GetAxis("Vertical");
			var direction = new Vector2(inputHorizontal, inputVertical).normalized;
			var playerTargetWalkingSpeed = direction * Config.PlayerWalkingRate;
			_mRigidbody2D.velocity = Vector2.Lerp(_mRigidbody2D.velocity, playerTargetWalkingSpeed,
				1 - Mathf.Exp(-Time.deltaTime * 10));
		}
	}
}
