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
	public partial class UIShipUIPackPanelManage : UIElement, IController
	{
		private List<BackPackItemTemplate> _backPackItemTemplates = new List<BackPackItemTemplate>();
		
		private IBackPackSystem _backPackSystem;

		private IUIGameShipPanelModel _uiGameShipPanelModel;

		private string _currentBackPackItemKey;

		private int _currentClickItemAmount;

		private void Awake()
		{
			_backPackSystem = this.GetSystem<IBackPackSystem>();
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
		}

		private void Start()
		{
			CloseButton.onClick.AddListener(() =>
			{
				_uiGameShipPanelModel.IfShipUIPackOpen.Value = false;
			});

			_uiGameShipPanelModel.IfItemInfoShow.Register(value =>
			{
				if (value)
				{
					ItemInfo.Show();
					ItemInfo.Name.text = _backPackSystem
						.BackPackItemInfos[_currentBackPackItemKey].ItemName;
					ItemInfo.Description.text = _backPackSystem
						.BackPackItemInfos[_currentBackPackItemKey].ItemDescription;
					ItemInfo.Amount.text = _currentClickItemAmount.ToString();
				}
				else
				{
					ItemInfo.Name.text = "";
					ItemInfo.Description.text = "";
					ItemInfo.Amount.text = "";
					ItemInfo.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{
			foreach (var (key, count) in _backPackSystem.ShipBackPackItemList)
			{
				BackPackItemTemplate.InstantiateWithParent(BackPackItemListRoot).Self(self =>
				{
					self.Icon.sprite = _backPackSystem.BackPackItemInfos[key].ItemIcon;
					self.Name.text = _backPackSystem.BackPackItemInfos[key].ItemName;
					self.Count.text = count.ToString();
					self.GetComponent<Button>().onClick.AddListener(() =>
					{
						_currentBackPackItemKey = key;
						_currentClickItemAmount = count;
						_uiGameShipPanelModel.IfItemInfoShow.Value = false;
						_uiGameShipPanelModel.IfItemInfoShow.Value = true;
						_backPackItemTemplates.Add(self);
					});
					self.Show();
				});
			}
		}

		private void OnDisable()
		{
			foreach (var backPackItemTemplate in _backPackItemTemplates)
			{
				backPackItemTemplate.gameObject.DestroySelf();
			}
			_backPackItemTemplates.Clear();
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