/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class PrimaryWeaponButton : UIElement, IController
	{
		private IWeaponSystem _weaponSystem;
		
		private void Awake()
		{
			_weaponSystem = this.GetSystem<IWeaponSystem>();
		}

		private void Start()
		{
			if (_weaponSystem.CurrentEquipWeapons[EquipWeaponKey.PrimaryWeapon] != null)
			{
				var weapon = _weaponSystem.CurrentEquipWeapons[EquipWeaponKey.PrimaryWeapon].GetComponent<Gun>();
				Name.text = weapon.weaponName;
				switch (weapon.bulletType)
				{
					case BulletType.Normal:
						Type.text = "普通子弹";
						break;
					case BulletType.Hypnosis:
						Type.text = "催眠子弹";
						break;
					default:
						Type.text = null;
						break;
				}

				weapon.currentAllAmmunition.Register(value =>
				{
					Ammunition.text = weapon.currentAmmunition + " / " + weapon.currentAllAmmunition;
				}).UnRegisterWhenGameObjectDestroyed(gameObject);
				weapon.currentAmmunition.Register(value =>
				{
					Ammunition.text = weapon.currentAmmunition + " / " + weapon.currentAllAmmunition;
				}).UnRegisterWhenGameObjectDestroyed(gameObject);
			}
			else
			{
				Name.text = "空";
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