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
	public partial class WeaponPanel : UIElement, IController
	{
		private IPlayerModel _playerModel;
		
		private void Awake()
		{
			_playerModel = this.GetModel<IPlayerModel>();
		}

		private void Start()
		{
			FishForkButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				_playerModel.CurrentWeaponType.Value = EquipWeaponKey.FishFork;
			});
			
			MeleeWeaponButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				_playerModel.CurrentWeaponType.Value = EquipWeaponKey.MeleeWeapon;
			});
			
			PrimaryWeaponButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				_playerModel.CurrentWeaponType.Value = EquipWeaponKey.PrimaryWeapon;
			});
			
			SecondaryWeaponButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				_playerModel.CurrentWeaponType.Value = EquipWeaponKey.SecondaryWeapons;
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