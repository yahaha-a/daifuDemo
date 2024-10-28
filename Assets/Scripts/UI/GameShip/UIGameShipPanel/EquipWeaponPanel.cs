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
	public partial class EquipWeaponPanel : UIElement, IController
	{
		private IUIGameShipPanelModel _uiGameShipPanelModel;
		
		private void Awake()
		{
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
		}

		private void Start()
		{
			FishFork.onClick.AddListener((() =>
			{
				_uiGameShipPanelModel.CurrentEquipWeaponKey.Value = EquipWeaponKey.FishFork;
				_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value = null;
			}));
			
			Knife.onClick.AddListener(() =>
			{
				_uiGameShipPanelModel.CurrentEquipWeaponKey.Value = EquipWeaponKey.MeleeWeapon;
				_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value = null;
			});
			
			PrimaryWeapon.onClick.AddListener(() =>
			{
				_uiGameShipPanelModel.CurrentEquipWeaponKey.Value = EquipWeaponKey.PrimaryWeapon;
				_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value = null;
			});
			
			SecondaryWeapons.onClick.AddListener(() =>
			{
				_uiGameShipPanelModel.CurrentEquipWeaponKey.Value = EquipWeaponKey.SecondaryWeapons;
				_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value = null;
			});
			
			_uiGameShipPanelModel.CurrentSelectWeaponInfo.RegisterWithInitValue(value =>
			{
				if (value != null)
				{
					DetailsPanel.Show();
				}
				else
				{
					DetailsPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGameShipPanelModel.CurrentEquipFishFork.RegisterWithInitValue(value =>
			{
				if (value == null)
				{
					FishForkName.text = "空";
				}
				else
				{
					FishForkName.text = value.Name;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_uiGameShipPanelModel.CurrentEquipMeleeWeapon.RegisterWithInitValue(value =>
			{
				if (value == null)
				{
					MeleeWeaponName.text = "空";
				}
				else
				{
					MeleeWeaponName.text = value.Name;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_uiGameShipPanelModel.CurrentEquipPrimaryWeapon.RegisterWithInitValue(value =>
			{
				if (value == null)
				{
					PrimaryWeaponName.text = "空";
				}
				else
				{
					PrimaryWeaponName.text = value.Name;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_uiGameShipPanelModel.CurrentEquipSecondaryWeapons.RegisterWithInitValue(value =>
			{
				if (value == null)
				{
					SecondaryWeaponName.text = "空";
				}
				else
				{
					SecondaryWeaponName.text = value.Name;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Confirm.onClick.AddListener(() =>
			{
				_uiGameShipPanelModel.IfEquipWeaponPanelOpen.Value = false;
				_uiGameShipPanelModel.IfSelectMapPanelShow.Value = true;
			});
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