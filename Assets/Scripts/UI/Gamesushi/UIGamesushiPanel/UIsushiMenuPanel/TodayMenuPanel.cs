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
	public partial class TodayMenuPanel : UIElement, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;
		
		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			for (int i = 0; i < 6; i++)
			{
				AddMenuButton.InstantiateWithParent(TodayMeunListRoot).Self(self =>
				{
					self.Show();
					self.onClick.AddListener(() =>
					{
						_uiGamesushiPanelModel.IfUIMenuPanelShow.Value = true;
					});
				});
			}
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