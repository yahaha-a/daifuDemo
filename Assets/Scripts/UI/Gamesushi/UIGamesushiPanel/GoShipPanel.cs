/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public partial class GoShipPanel : UIElement, IController
	{
		private void Awake()
		{
			ConfirmButton.onClick.AddListener(() =>
			{
				this.SendCommand<OpenOrCloseGoShipPanelCommand>();
				UIKit.ClosePanel<UIGamesushiPanel>();
				SceneManager.LoadScene("GameShip");
			});
			
			CancelButton.onClick.AddListener(() =>
			{
				this.SendCommand<OpenOrCloseGoShipPanelCommand>();
			});
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}