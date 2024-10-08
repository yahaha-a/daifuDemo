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
	public partial class UIPackItemDetailPanel : UIElement, IController
	{
		private IUIGamePanelModel _uiGamePanelModel;

		private IBackPackSystem _backPackSystem;
		
		private void Awake()
		{
			_uiGamePanelModel = this.GetModel<IUIGamePanelModel>();
			_backPackSystem = this.GetSystem<IBackPackSystem>();

			_uiGamePanelModel.CurrentSelectItemKey.Register(key =>
			{
				Icon.sprite = _backPackSystem.BackPackItemInfos[key].ItemIcon;
				Name.text = _backPackSystem.BackPackItemInfos[key].ItemName;
				Detail.text = _backPackSystem.BackPackItemInfos[key].ItemDescription;
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