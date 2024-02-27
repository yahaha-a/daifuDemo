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

			_uiGamesushiPanelModel.SelectedMenuItemKey.RegisterWithInitValue(key =>
			{
				foreach (var needFoodObject in _needFoodObjects)
				{
					needFoodObject.DestroySelf();
				}
				_needFoodObjects.Clear();
				
				Name.text = _menuSystem.MenuItemInfos[key].Name;
				Icon.sprite = _menuSystem.MenuItemInfos[key].Icon;
				Rank.text = "Lv." + _menuSystem.CurrentOwnMenuItems[key].Rank.ToString();
				Price.text = "$ " + _menuSystem.MenuItemInfos[key].RankWithCost
					.Where(rankAndCost => rankAndCost.Item1 == _menuSystem.CurrentOwnMenuItems[key].Rank)
					.Select(rankAndCost => rankAndCost.Item2).FirstOrDefault();
				Describe.text = _menuSystem.MenuItemInfos[key].Description;
				foreach (var (backPackKey, amount) in _menuSystem.MenuItemInfos[key].RequiredIngredientsAmount)
				{
					NeedFoodTemplate.InstantiateWithParent(NeedFoodRoot).Self(self =>
					{
						self.Icon.sprite = _backPackSystem.BackPackItemInfos[backPackKey].ItemIcon;
						self.Name.text = _backPackSystem.BackPackItemInfos[backPackKey].ItemName;
						self.OwnAndNeedAmount.text = _backPackSystem.SuShiBackPackItemList[backPackKey] + "/" + amount;
						self.Show();
						_needFoodObjects.Add(self.gameObject);
					});
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