using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class GamesushiController : ViewController, IController
	{
		private void Awake()
		{
			ResKit.Init();
			UIKit.Root.SetResolution(1920, 1080, 1);
			UIKit.OpenPanel<UIGamesushiPanel>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.SendCommand<OpenOrClosesushiIngredientPanel>();
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
