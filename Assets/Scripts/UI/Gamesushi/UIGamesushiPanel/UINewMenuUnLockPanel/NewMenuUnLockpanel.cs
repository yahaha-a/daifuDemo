/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class NewMenuUnLockpanel : UIElement, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private IMenuSystem _menuSystem;

		private ICollectionModel _collectionModel;
		
		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_menuSystem = this.GetSystem<IMenuSystem>();

			_collectionModel = this.GetModel<ICollectionModel>();

			_uiGamesushiPanelModel.CurrentSelectLockMenuItemKey.RegisterWithInitValue(menuKey =>
			{
				ReFreshShow(menuKey);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			ConfirmButton.onClick.AddListener(() =>
			{
				if (_menuSystem.IfCanUnlockMenu(_uiGamesushiPanelModel.CurrentSelectLockMenuItemKey.Value))
				{
					_menuSystem.UnlockMenu(_uiGamesushiPanelModel.CurrentSelectLockMenuItemKey.Value);
					_uiGamesushiPanelModel.CurrentSelectLockMenuItemKey.Value = null;
				}
			});
			
			CloseButton.onClick.AddListener(() =>
			{
				this.SendCommand<OpenOrCloseNewMenuUnlockPanelCommand>();
			});
		}

		private void ReFreshShow(string key)
		{
			if (key == null)
			{
				NotSelectShow.Show();
				SelectShow.Hide();
			}
			else
			{
				SelectShow.Show();
				NotSelectShow.Hide();
				
				Name.text = _menuSystem.MenuItemInfos[key].Name;
				Icon.sprite = _menuSystem.MenuItemInfos[key].Icon;
				Gold.text = "$: " + _menuSystem.MenuItemInfos[key].RankWithCost.FirstOrDefault(item => item.Item1 == 1)
					.Item2;
				Score.text = "评分: " + _menuSystem.MenuItemInfos[key].Score;
				Dishes.text = _menuSystem.MenuItemInfos[key].Copies + "盘";
				Description.text = _menuSystem.MenuItemInfos[key].Description;
				NeedAndHaveGold.text = _menuSystem.MenuItemInfos[key].UnLockNeed + "/" + _collectionModel.Gold;
			}
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