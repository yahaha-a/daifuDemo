using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class GameShipController : ViewController, IController
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.B))
			{
				this.SendCommand<OpenOrClosesuShiBackPackCommand>();
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
