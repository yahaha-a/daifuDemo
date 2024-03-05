using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class GoShip : ViewController, IController
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("PlayerInteractionBox"))
			{
				this.SendCommand<OpenOrCloseGoShipPanelCommand>();
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag("PlayerInteractionBox"))
			{
				this.SendCommand<OpenOrCloseGoShipPanelCommand>();
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
