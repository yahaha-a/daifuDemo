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
	public partial class MenuDetail : UIElement, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private IBackPackSystem _backPackSystem;

		private IMenuSystem _menuSystem;

		private List<GameObject> _needFoodObjects = new List<GameObject>();
		
		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_backPackSystem = this.GetSystem<IBackPackSystem>();

			_menuSystem = this.GetSystem<IMenuSystem>();
			
			ConfirmButton.onClick.AddListener(() =>
			{
				_uiGamesushiPanelModel.IfUISelectMenuAmountPanelShow.Value = true;
			});
			
			UpgrateButton.onClick.AddListener(() =>
			{
				_uiGamesushiPanelModel.IfUIUpgradeMenuPanelShow.Value = true;
			});

			_uiGamesushiPanelModel.SelectedMenuItemKey.RegisterWithInitValue(key =>
			{
				UpdatePanel();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.UpgradeMenu.Register(() =>
			{
				UpdatePanel();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{
			var key = _uiGamesushiPanelModel.SelectedMenuItemKey.Value;
				
			Name.text = _menuSystem.MenuItemInfos[key].Name;
			Icon.sprite = _menuSystem.MenuItemInfos[key].Icon;
			Rank.text = "Lv." + _menuSystem.CurrentOwnMenuItems[key].Rank.ToString();
			Price.text = "$ " + _menuSystem.MenuItemInfos[key].RankWithCost
				.Where(rankAndCost => rankAndCost.Item1 == _menuSystem.CurrentOwnMenuItems[key].Rank)
				.Select(rankAndCost => rankAndCost.Item2).FirstOrDefault();
			Evaluate.text = "评分: " + _menuSystem.MenuItemInfos[key].Score;
			Copies.text = _menuSystem.MenuItemInfos[key].Copies + " 盘";
				
			Describe.text = _menuSystem.MenuItemInfos[key].Description;
				
			foreach (var (rank, backPackKey, amount) in _menuSystem.MenuItemInfos[key].UpgradeNeedItems)
			{
				if (_menuSystem.CurrentOwnMenuItems[key].Rank == rank)
				{
					NeedFoodTemplate.InstantiateWithParent(NeedFoodRoot).Self(self =>
					{
						self.Icon.sprite = _backPackSystem.BackPackItemInfos[backPackKey].ItemIcon;
						self.Name.text = _backPackSystem.BackPackItemInfos[backPackKey].ItemName;
						self.OwnAndNeedAmount.text = amount + "/" + _backPackSystem.SuShiBackPackItemList[backPackKey];
						self.Show();
						_needFoodObjects.Add(self.gameObject);
					});
				}
			}
		}

		private void OnDisable()
		{
			foreach (var needFoodObject in _needFoodObjects)
			{
				needFoodObject.DestroySelf();
			}
			_needFoodObjects.Clear();
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