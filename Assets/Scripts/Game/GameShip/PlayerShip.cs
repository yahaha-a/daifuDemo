using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class PlayerShip : ViewController
	{
		private Rigidbody2D _rigidbody2D;

		private float _speed = 3f;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		private void Start()
		{
			ResKit.Init();
			UIKit.Root.SetResolution(1920, 1080, 1);
			UIKit.OpenPanel<UIGameShipPanel>();
		}

		private void Update()
		{
			var inputHorizontal = Input.GetAxis("Horizontal");
    
			var direction = new Vector2(inputHorizontal, 0).normalized;
    
			var playerTargetWalkingSpeed = direction * _speed;
			_rigidbody2D.linearVelocity = Vector2.Lerp(_rigidbody2D.linearVelocity, playerTargetWalkingSpeed,
				1 - Mathf.Exp(-Time.deltaTime * 10));
		}

	}
}
