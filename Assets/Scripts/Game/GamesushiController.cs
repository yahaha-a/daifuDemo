using System;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace daifuDemo
{
	public partial class GamesushiController : ViewController, IController
	{
		private float _pressKeyCodeETime = 0;
		
		private void Awake()
		{
			ResKit.Init();
			UIKit.Root.SetResolution(1920, 1080, 1);
			UIKit.OpenPanel<UIGamesushiPanel>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.SendCommand<OpenOrClosesushiMenuPanel>();
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.SendCommand<OpenOrClosesushiIngredientPanel>();
			}
			
			if (Input.GetKey(KeyCode.E))
			{
				_pressKeyCodeETime += Time.deltaTime;
				if (_pressKeyCodeETime >= 2f)
				{
					Events.CommencedBusiness?.Trigger();
				}
			}

			if (Input.GetKeyUp(KeyCode.E))
			{
				_pressKeyCodeETime = 0;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
