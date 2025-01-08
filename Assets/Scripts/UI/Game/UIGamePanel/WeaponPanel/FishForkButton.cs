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
	public partial class FishForkButton : UIElement, IController
	{
		private IWeaponSystem _weaponSystem;
		
		private void Awake()
		{
			_weaponSystem = this.GetSystem<IWeaponSystem>();
		}

		private void Start()
		{
			if (_weaponSystem.CurrentEquipWeapons[EquipWeaponKey.FishFork] != null)
			{
				var weapon = _weaponSystem.CurrentEquipWeapons[EquipWeaponKey.FishFork].GetComponent<FishFork>();
				Name.text = weapon.weaponName;
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