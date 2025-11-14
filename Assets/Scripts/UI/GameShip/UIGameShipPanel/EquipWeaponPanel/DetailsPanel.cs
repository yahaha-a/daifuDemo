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
	public partial class DetailsPanel : UIElement, IController
	{
		private IUIGameShipPanelModel _uiGameShipPanelModel;
		private IWeaponSystem _weaponSystem;
		
		private void Awake()
		{
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			_weaponSystem = this.GetSystem<IWeaponSystem>();
		}

		private void Start()
		{
			_uiGameShipPanelModel.CurrentEquipWeaponKey.RegisterWithInitValue(value =>
			{
				if (value == EquipWeaponKey.FishFork)
				{
					SelectBulletTypePanel.Hide();
				}
				else if (value == EquipWeaponKey.MeleeWeapon)
				{
					SelectBulletTypePanel.Hide();
				}
				else if (value == EquipWeaponKey.PrimaryWeapon)
				{
					SelectBulletTypePanel.Show();
				}
				else if (value == EquipWeaponKey.SecondaryWeapons)
				{
					SelectBulletTypePanel.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_uiGameShipPanelModel.CurrentSelectWeaponInfo.RegisterWithInitValue(weaponInfo =>
			{
				if (weaponInfo != null)
				{
					Name.text = weaponInfo.Name;
				}
			});
			
			Equip.onClick.AddListener(() =>
			{
				_weaponSystem.UpdateEquipWeapon(EquipWeaponKey.FishFork, _uiGameShipPanelModel.CurrentEquipFishFork);
				_weaponSystem.UpdateEquipWeapon(EquipWeaponKey.MeleeWeapon, _uiGameShipPanelModel.CurrentEquipMeleeWeapon);
				_weaponSystem.UpdateEquipWeapon(EquipWeaponKey.PrimaryWeapon, _uiGameShipPanelModel.CurrentEquipPrimaryWeapon);
				_weaponSystem.UpdateEquipWeapon(EquipWeaponKey.SecondaryWeapons, _uiGameShipPanelModel.CurrentEquipSecondaryWeapons);

				if (_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.EquipState.Value != EquipWeaponKey.Null &&
				    _uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.EquipState.Value !=
				    _uiGameShipPanelModel.CurrentEquipWeaponKey.Value)
				{
					_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.WithEquipState(EquipWeaponKey.Null);
				}
			});

			_uiGameShipPanelModel.IfCurrentSelectWeaponEquip.RegisterWithInitValue(value =>
			{
				if (value)
				{
					EquipText.text = "卸下";
				}
				else
				{
					EquipText.text = "装备";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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