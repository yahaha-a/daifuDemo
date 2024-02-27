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
	public partial class MenuPanel : UIElement, IController
	{
		private IMenuSystem _menuSystem;

		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private List<GameObject> _menuItems = new List<GameObject>();

		private void Awake()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
		}

		private void OnEnable()
		{
			int i = 0;
			foreach (var (key, currentOwnMenuItem) in _menuSystem.CurrentOwnMenuItems)
			{
				if (currentOwnMenuItem.Unlock)
				{
					MenuTemplate.InstantiateWithParent(MenuListRoot).Self(self =>
					{
						self.Icon.sprite = _menuSystem.MenuItemInfos[key].Icon;
						self.Rank.text = "Lv." + currentOwnMenuItem.Rank.ToString();
						self.Amount.text = currentOwnMenuItem.CanMakeNumber.ToString();
						string currentKey = key;
						self.GetComponent<Button>().onClick.AddListener(() =>
						{
							_uiGamesushiPanelModel.SelectedMenuItemKey.Value = currentKey;
						});
						if (i == 0)
						{
							_uiGamesushiPanelModel.SelectedMenuItemKey.Value = currentKey;
						}
						self.Show();
						_menuItems.Add(self.gameObject);
					});
				}
			}
		}

		protected override void OnBeforeDestroy()
		{
			foreach (var menuItem in _menuItems)
			{
				menuItem.DestroySelf();
			}
			_menuItems.Clear();
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}