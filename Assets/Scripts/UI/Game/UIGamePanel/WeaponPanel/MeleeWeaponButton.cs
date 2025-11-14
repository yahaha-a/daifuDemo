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
	public partial class MeleeWeaponButton : UIElement, IController
	{
		private IWeaponSystem _weaponSystem;
		
		private void Awake()
		{
			_weaponSystem = this.GetSystem<IWeaponSystem>();
		}

		private void Start()
		{
			if (_weaponSystem.CurrentEquipWeapons[EquipWeaponKey.MeleeWeapon] != null)
			{
				var weapon = _weaponSystem.CurrentEquipWeapons[EquipWeaponKey.MeleeWeapon].GetComponent<MeleeWeapon>();
				Name.text = weapon.weaponName;
				
				weapon.currentRank.Register(rank =>
				{
					if (rank == weapon.MaxRank)
					{
						Level.text = "Lv.Max";
					}
					else
					{
						Level.text = "Lv." + rank;
					}
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