/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SelectBulletTypePanel : UIElement, IController
	{
		private IUIGameShipPanelModel _uiGameShipPanelModel;
		private ICollectionModel _collectionModel;
		private IBulletSystem _bulletSystem;

		private List<IBulletInfo> _bulletInfos = new List<IBulletInfo>();
		
		private void Awake()
		{
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			_collectionModel = this.GetModel<ICollectionModel>();
			_bulletSystem = this.GetSystem<IBulletSystem>();
		}

		private void Start()
		{
			_uiGameShipPanelModel.CurrentSelectWeaponInfo.Register(weaponInfo =>
			{
				if (weaponInfo != null)
				{
					_bulletInfos.Clear();
					SelectBulletList.options.Clear();
					foreach (IBulletInfo bulletInfo in _bulletSystem.BulletInfos[weaponInfo.Key])
					{
						_bulletInfos.Add(bulletInfo);
					}
					if (_bulletInfos.Count > 0)
					{
						_uiGameShipPanelModel.CurrentSelectBulletInfo.Value = _bulletInfos[0];
						
						foreach (IBulletInfo bulletInfo in _bulletInfos)
						{
							SelectBulletList.options.Add(new Dropdown.OptionData(bulletInfo.Name));
						}
					}
				
					SelectBulletList.value = 0;
					SelectBulletList.RefreshShownValue();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			SelectBulletList.onValueChanged.AddListener(OnBulletTypeSelected);
			
			Count.contentType = InputField.ContentType.IntegerNumber;
			Count.onValueChanged.AddListener(ValidateInput);
			
			_uiGameShipPanelModel.CurrentInputAmmunitionNumber.RegisterWithInitValue(number =>
			{
				if (number == 0)
				{
					AllPrice.text = "总价: " + 0 + "金币";
				}
				else
				{
					AllPrice.text = "总价: " + number * _uiGameShipPanelModel.CurrentSelectBulletInfo.Value.Price + "金币";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			CountName.text = "弹药数量 " + _uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.AmmunitionNumber.Value;
			
			Confirm.onClick.AddListener(() =>
			{
				if (_collectionModel.Gold.Value >= _uiGameShipPanelModel.CurrentInputAmmunitionNumber.Value)
				{
					_collectionModel.Gold.Value -= _uiGameShipPanelModel.CurrentInputAmmunitionNumber.Value;
					_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.AmmunitionNumber.Value +=
						_uiGameShipPanelModel.CurrentInputAmmunitionNumber.Value;
					
					CountName.text = "弹药数量 " + _uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.AmmunitionNumber.Value;
					Count.text = "购买弹药";
					_uiGameShipPanelModel.CurrentInputAmmunitionNumber.Value = 0;
				}
			});
		}
		
		private void OnBulletTypeSelected(int index)
		{
			_uiGameShipPanelModel.CurrentSelectBulletInfo.Value = _bulletInfos[index];
		}
		
		private void ValidateInput(string text)
		{
			int maxDigits = 2;

			if (string.IsNullOrEmpty(text))
			{
				Count.text = "购买弹药";
				_uiGameShipPanelModel.CurrentInputAmmunitionNumber.Value = 0;
				return;
			}

			if (text.Length > maxDigits)
			{
				text = text.Substring(0, maxDigits);
			}

			if (int.TryParse(text, out int number))
			{
				if (number <= 0)
				{
					Count.text = "购买弹药";
					_uiGameShipPanelModel.CurrentInputAmmunitionNumber.Value = 0;
				}
				else
				{
					_uiGameShipPanelModel.CurrentInputAmmunitionNumber.Value = number;
				}
			}
			else
			{
				Count.text = "购买弹药";
				_uiGameShipPanelModel.CurrentInputAmmunitionNumber.Value = 0;
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