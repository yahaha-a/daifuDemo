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
		private IBulletSystem _bulletSystem;

		private List<IBulletInfo> _bulletInfos = new List<IBulletInfo>();
		
		private void Awake()
		{
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			_bulletSystem = this.GetSystem<IBulletSystem>();
		}

		private void Start()
		{
			_uiGameShipPanelModel.CurrentSelectBulletInfo.Register(bulletInfo =>
			{
				if (bulletInfo != null)
				{
					CountName.text = "弹药数量 " + bulletInfo.InitNumber;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_uiGameShipPanelModel.CurrentSelectWeaponInfo.RegisterWithInitValue(weaponInfo =>
			{
				if (weaponInfo != null && _bulletSystem.BulletInfos.ContainsKey(weaponInfo.Key))
				{
					_bulletInfos.Clear();
					SelectBulletList.options.Clear();
					foreach (IBulletInfo bulletInfo in _bulletSystem.BulletInfos[weaponInfo.Key])
					{
						_bulletInfos.Add(bulletInfo);
					}
					if (_bulletInfos.Count > 0)
					{
						foreach (IBulletInfo bulletInfo in _bulletInfos)
						{
							SelectBulletList.options.Add(new Dropdown.OptionData(bulletInfo.Name));
						}
					}

					if (weaponInfo.CurrentBulletInfo == null)
					{
						weaponInfo.WithBullet(_bulletInfos[0]);
					}
					_uiGameShipPanelModel.CurrentSelectBulletInfo.Value = weaponInfo.CurrentBulletInfo;
					_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.WithBullet(weaponInfo.CurrentBulletInfo);
					SelectBulletList.value =
						_bulletInfos.IndexOf(weaponInfo.CurrentBulletInfo);
					SelectBulletList.RefreshShownValue();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			SelectBulletList.onValueChanged.AddListener(OnBulletTypeSelected);
		}
		
		private void OnBulletTypeSelected(int index)
		{
			_uiGameShipPanelModel.CurrentSelectBulletInfo.Value = _bulletInfos[index];
			_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.WithBullet(_bulletInfos[index]);
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