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

		private IMenuSystem _menuSystem;

		private List<GameObject> _todayMenuItems = new List<GameObject>();
		
		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_menuSystem = this.GetSystem<IMenuSystem>();

			Events.UpgradeMenu.Register(() =>
			{
				UpdatePanel();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{
			foreach (var todayMenuItem in _menuSystem.TodayMenuItems)
			{
				if (todayMenuItem.Key == null)
				{
					AddMenuButton.InstantiateWithParent(TodayMeunListRoot).Self(self =>
					{
						self.Show();
						self.onClick.AddListener(() =>
						{
							_uiGamesushiPanelModel.IfUIMenuPanelShow.Value = true;
							_uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Value = todayMenuItem.Node;
						});
						_todayMenuItems.Add(self.gameObject);
					});
				}
				else
				{
					TodayMenuTemplate.InstantiateWithParent(TodayMeunListRoot).Self(self =>
					{
						self.Icon.sprite = _menuSystem.MenuItemInfos[todayMenuItem.Key].Icon;
						self.Name.text = _menuSystem.MenuItemInfos[todayMenuItem.Key].Name;
						self.Rank.text = "Lv." + _menuSystem.CurrentOwnMenuItems[todayMenuItem.Key].Rank.ToString();
						self.Amount.text = "×" + todayMenuItem.Amount.ToString();
						self.Show();
						_todayMenuItems.Add(self.gameObject);
					});
				}
			}
		}

		private void OnDisable()
		{
			foreach (var todayMenuItem in _todayMenuItems)
			{
				todayMenuItem.DestroySelf();
			}
			_todayMenuItems.Clear();
		}

		protected override void OnBeforeDestroy()
		{
		}
		
		void UpdatePanel()
		{
			this.gameObject.Hide();
			this.gameObject.Show();
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}