/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
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

			_uiGamesushiPanelModel.SelectedMenuItemKey.Value = _menuSystem.CurrentOwnMenuItems.Keys.FirstOrDefault();
			
			CloseButton.onClick.AddListener(() =>
			{
				_uiGamesushiPanelModel.IfUIMenuPanelShow.Value = false;
			});
		}

		private void OnEnable()
		{
			foreach (var (key, currentOwnMenuItem) in _menuSystem.CurrentOwnMenuItems)
			{
				MenuTemplate.InstantiateWithParent(MenuListRoot).Self(self =>
				{
					self.CurrentOwnMenuItem = currentOwnMenuItem;
					self.Show();
					_menuItems.Add(self.gameObject);
				});
			}
		}

		private void OnDisable()
		{
			foreach (var menuItem in _menuItems)
			{
				menuItem.DestroySelf();
			}
			_menuItems.Clear();
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