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
	public partial class UpgradeMenuPanel : UIElement, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private IBackPackSystem _backPackSystem;
		
		private IMenuSystem _menuSystem;

		private List<GameObject> _upgradeNeedFoodItems = new List<GameObject>();
		
		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_backPackSystem = this.GetSystem<IBackPackSystem>();

			_menuSystem = this.GetSystem<IMenuSystem>();
			
			CloseButton.onClick.AddListener(() =>
			{
				_uiGamesushiPanelModel.IfUIUpgradeMenuPanelShow.Value = false;
			});
			
			ConfirmButton.onClick.AddListener(() =>
			{
				_menuSystem.UpgradeMenu(_uiGamesushiPanelModel.SelectedMenuItemKey.Value);
				UpdatePanel();
				Events.UpgradeMenu?.Trigger();
			});
		}

		private void OnEnable()
		{
			Icon.sprite = _menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Icon;
			CurrentData.Rank.text =
				"Lv." + _menuSystem.CurrentOwnMenuItems[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Rank;
			CurrentData.Cost.text = "$ " + _menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value]
				.RankWithCost
				.Where(rankWithCost => rankWithCost.Item1 ==
				                       _menuSystem.CurrentOwnMenuItems[_uiGamesushiPanelModel.SelectedMenuItemKey.Value]
					                       .Rank).Select(rankWithCost => rankWithCost.Item2).FirstOrDefault();
			CurrentData.Score.text =
				"评分: " + _menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Score;
			CurrentData.Copies.text =
				_menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Copies + " 盘";

			UpgradeData.Rank.text = "Lv." +
			                        (_menuSystem.CurrentOwnMenuItems[_uiGamesushiPanelModel.SelectedMenuItemKey.Value]
				                        .Rank + 1);
			UpgradeData.Cost.text = "$ " + _menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value]
				.RankWithCost
				.Where(rankWithCost => rankWithCost.Item1 ==
				                       _menuSystem.CurrentOwnMenuItems[_uiGamesushiPanelModel.SelectedMenuItemKey.Value]
					                       .Rank + 1).Select(rankWithCost => rankWithCost.Item2).FirstOrDefault();
			CurrentData.Score.text =
				"评分: " + _menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Score;
			CurrentData.Copies.text =
				_menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Copies + " 盘";
			
			foreach (var (rank, backPackKey, amount) in _menuSystem
				         .MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].UpgradeNeedItems)
			{
				if (_menuSystem.CurrentOwnMenuItems[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Rank == rank)
				{
					UpgradeNeedFooditemTemplte.InstantiateWithParent(NeeedFoodListRoot).Self(self =>
					{
						self.Icon.sprite = _backPackSystem.BackPackItemInfos[backPackKey].ItemIcon;
						self.Name.text = _backPackSystem.BackPackItemInfos[backPackKey].ItemName;
						self.NeedAndOwnAmount.text = amount + "/" + _backPackSystem.SuShiBackPackItemList[backPackKey];
						self.Show();
						_upgradeNeedFoodItems.Add(self.gameObject);
					});
				}
			}
		}

		private void OnDisable()
		{
			foreach (var upgradeNeedFoodItem in _upgradeNeedFoodItems)
			{
				upgradeNeedFoodItem.DestroySelf();
			}
			_upgradeNeedFoodItems.Clear();
		}

		void UpdatePanel()
		{
			this.gameObject.Hide();
			this.gameObject.Show();
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