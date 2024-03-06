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
	public partial class MenuTemplate : UIElement, IController
	{
		private IMenuSystem _menuSystem;

		private IUIGamesushiPanelModel _uiGamesushiPanelModel;
		
		public ICurrentOwnMenuItemInfo CurrentOwnMenuItem { get; set; }
		
		private void Awake()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
			
			GetComponent<Button>().onClick.AddListener(() =>
			{
				_uiGamesushiPanelModel.SelectedMenuItemKey.Value = CurrentOwnMenuItem.Key.Value;
			});
			
			_menuSystem.CalculateCanMakeNumber(CurrentOwnMenuItem);
			
			RefreshShow();

			CurrentOwnMenuItem.Rank.Register(rank =>
			{
				RefreshShow();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			CurrentOwnMenuItem.CanMakeNumber.Register(value =>
			{
				RefreshShow();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		void RefreshShow()
		{
			Icon.sprite = _menuSystem.MenuItemInfos[CurrentOwnMenuItem.Key.Value].Icon;
			Rank.text = "Lv." + CurrentOwnMenuItem.Rank.Value;
			Amount.text = CurrentOwnMenuItem.CanMakeNumber.Value.ToString();
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