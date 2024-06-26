/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UISettlePanel : UIElement, IController
	{
		private IUIGamePanelModel _uiGamePanelModel;
		
		private void Awake()
		{
			_uiGamePanelModel = this.GetModel<IUIGamePanelModel>();
			
			ConfirmButton.onClick.AddListener(() =>
			{
				this.Hide();
				_uiGamePanelModel.IfUIHarvestPanelShow.Value = true;
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