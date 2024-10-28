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
	public partial class SelectWeaponPanel : UIElement, IController
	{
		private IUIGameShipPanelModel _uiGameShipPanelModel;
		private IWeaponSystem _weaponSystem;

		private List<IWeaponItemTempleteInfo> _weaponItemTempleteInfos = new List<IWeaponItemTempleteInfo>();
		private List<GameObject> _weaponItems = new List<GameObject>();
		
		private void Awake()
		{
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			_weaponSystem = this.GetSystem<IWeaponSystem>();
		}

		private void Start()
		{
			foreach (var (key, rank) in _weaponSystem.WeaponOwnInfos)
			{
				IWeaponInfo weaponInfo = _weaponSystem.WeaponInfos[(key, rank)];
				IWeaponItemTempleteInfo weaponItemTempleteInfo = new WeaponItemTempleteInfo();
				weaponItemTempleteInfo.WithKey(weaponInfo.Key);
				weaponItemTempleteInfo.WithName(weaponInfo.Name);
				weaponItemTempleteInfo.WithRank(weaponInfo.Rank);
				_weaponItemTempleteInfos.Add(weaponItemTempleteInfo);
			}

			_uiGameShipPanelModel.CurrentEquipWeaponKey.Register(value =>
			{
				foreach (GameObject weaponItem in _weaponItems)
				{
					weaponItem.DestroySelf();
				}
				_weaponItems.Clear();
				
				List<IWeaponInfo> weaponInfos = _weaponSystem.FindObtainWeaponInfos(value);

				foreach (IWeaponInfo weaponInfo in weaponInfos)
				{
					WeaponItemTemplete.InstantiateWithParent(WeaponItemRoot).Self(self =>
					{
						IWeaponItemTempleteInfo weaponItemTempleteInfo =
							_weaponItemTempleteInfos.Find(t => t.Key == weaponInfo.Key);
						
						self.WeaponItemTempleteInfo = weaponItemTempleteInfo;
						self.Show();

						_weaponItems.Add(self.gameObject);
					});
				}
				
				this.GetUtility<IUtils>().AdjustContentHeight(WeaponItemRoot);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnDisable()
		{
			foreach (GameObject weaponItem in _weaponItems)
			{
				weaponItem.DestroySelf();
			}
			_weaponItems.Clear();
			
			_weaponItemTempleteInfos.Clear();
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