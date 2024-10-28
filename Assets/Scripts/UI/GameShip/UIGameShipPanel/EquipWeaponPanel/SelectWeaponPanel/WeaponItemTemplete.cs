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
	public partial class WeaponItemTemplete : UIElement, IController
	{
		public IWeaponItemTempleteInfo WeaponItemTempleteInfo;

		private IUIGameShipPanelModel _uiGameShipPanelModel;

		private EquipWeaponKey _oldEquipState = EquipWeaponKey.Null;

		private void Awake()
		{
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
		}

		private void Start()
		{
			Name.text = WeaponItemTempleteInfo.Name;

			WeaponItemTempleteInfo.EquipState.RegisterWithInitValue(state =>
			{
				if (state == EquipWeaponKey.Null)
				{
					EquipState.text = null;
					_uiGameShipPanelModel.IfCurrentSelectWeaponEquip.Value = false;
				}
				else if (state == EquipWeaponKey.FishFork)
				{
					EquipState.text = "已装备\n鱼叉";
					_uiGameShipPanelModel.IfCurrentSelectWeaponEquip.Value = true;
				}
				else if (state == EquipWeaponKey.MeleeWeapon)
				{
					EquipState.text = "已装备\n近战武器";
					_uiGameShipPanelModel.IfCurrentSelectWeaponEquip.Value = true;
				}
				else if (state == EquipWeaponKey.PrimaryWeapon)
				{
					EquipState.text = "已装备\n主武器";
					_uiGameShipPanelModel.IfCurrentSelectWeaponEquip.Value = true;
				}
				else if (state == EquipWeaponKey.SecondaryWeapons)
				{
					EquipState.text = "已装备\n副武器";
					_uiGameShipPanelModel.IfCurrentSelectWeaponEquip.Value = true;
				}

				if (_oldEquipState == EquipWeaponKey.Null && state != EquipWeaponKey.Null)
				{
					if (state == EquipWeaponKey.FishFork)
					{
						_uiGameShipPanelModel.CurrentEquipFishFork.Value = WeaponItemTempleteInfo;
					}
					else if (state == EquipWeaponKey.MeleeWeapon)
					{
						_uiGameShipPanelModel.CurrentEquipMeleeWeapon.Value = WeaponItemTempleteInfo;
					}
					else if (state == EquipWeaponKey.PrimaryWeapon)
					{
						_uiGameShipPanelModel.CurrentEquipPrimaryWeapon.Value = WeaponItemTempleteInfo;
					}
					else if (state == EquipWeaponKey.SecondaryWeapons)
					{
						_uiGameShipPanelModel.CurrentEquipSecondaryWeapons.Value = WeaponItemTempleteInfo;
					}
				}
				else if (_oldEquipState != EquipWeaponKey.Null && state == EquipWeaponKey.Null)
				{
					if (_oldEquipState == EquipWeaponKey.FishFork)
					{
						_uiGameShipPanelModel.CurrentEquipFishFork.Value = null;
					}
					else if (_oldEquipState == EquipWeaponKey.MeleeWeapon)
					{
						_uiGameShipPanelModel.CurrentEquipMeleeWeapon.Value = null;
					}
					else if (_oldEquipState == EquipWeaponKey.PrimaryWeapon)
					{
						_uiGameShipPanelModel.CurrentEquipPrimaryWeapon.Value = null;
					}
					else if (_oldEquipState == EquipWeaponKey.SecondaryWeapons)
					{
						_uiGameShipPanelModel.CurrentEquipSecondaryWeapons.Value = null;
					}
				}

				_oldEquipState = state;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			
			GetComponent<Button>().onClick.AddListener(() =>
			{
				_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value = WeaponItemTempleteInfo;
				_uiGameShipPanelModel.IfCurrentSelectWeaponEquip.Value =
					WeaponItemTempleteInfo.EquipState.Value == EquipWeaponKey.Null ? false : true;
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