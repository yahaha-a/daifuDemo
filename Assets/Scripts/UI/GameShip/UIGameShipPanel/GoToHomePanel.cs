/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public partial class GoToHomePanel : UIElement, IController
	{
		private void Awake()
		{
			var uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			
			AgreeButton.onClick.AddListener(() =>
			{
				uiGameShipPanelModel.IfGoToHomePanelOpen.Value = false;
				SceneManager.LoadScene("Gamesushi");
			});
			
			CancelButton.onClick.AddListener(() =>
			{
				uiGameShipPanelModel.IfGoToHomePanelOpen.Value = false;
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