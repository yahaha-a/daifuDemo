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
	public partial class UIsushiMenuPanel : UIElement, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;
		
		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_uiGamesushiPanelModel.IfUIMenuPanelShow.Register(value =>
			{
				if (value)
				{
					MenuPanel.Show();
					MenuDetail.Show();
				}
				else
				{
					MenuPanel.Hide();
					MenuDetail.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{
			
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