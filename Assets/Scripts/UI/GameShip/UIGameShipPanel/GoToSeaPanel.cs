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
	public partial class GoToSeaPanel : UIElement, IController
	{
		private void Awake()
		{
			var uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			
			AgreeButton.onClick.AddListener(() =>
			{
				uiGameShipPanelModel.IfGotoSeaPanelOpen.Value = false;
				Events.SelectMap?.Trigger();
			});
			
			CancelButton.onClick.AddListener(() =>
			{
				uiGameShipPanelModel.IfGotoSeaPanelOpen.Value = false;
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