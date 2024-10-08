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
	public partial class UIPackItemTemplate : UIElement, IController
	{
		public string key;

		private IUIGamePanelModel _uiGamePanelModel;
		
		private void Awake()
		{
			_uiGamePanelModel = this.GetModel<IUIGamePanelModel>();
			
			this.GetComponent<Button>().onClick.AddListener(() =>
			{
				_uiGamePanelModel.CurrentSelectItemKey.Value = key;
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