using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class PlayerSuShi : ViewController, IController
	{
		private IBusinessModel _businessModel;
		
		private float _speed = 3f;

		private void Start()
		{
			_businessModel = this.GetModel<IBusinessModel>();

			_businessModel.CurrentTouchTableItemInfo.Register(value =>
			{
				if (value.CustomerItemInfo != null && value.CustomerItemInfo.CurrentOrderKey != null)
				{
					
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			var inputHorizontal = Input.GetAxis("Horizontal");
			var inputVertical = Input.GetAxis("Vertical");
			
			var direction = new Vector2(inputHorizontal, inputVertical).normalized;
			var playerTargetWalkingSpeed = direction * _speed;
			Rigidbody2d.velocity = playerTargetWalkingSpeed;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
